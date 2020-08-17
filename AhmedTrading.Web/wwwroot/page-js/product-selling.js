
//date picker
$('.datepicker').pickadate().val(moment(new Date()).format('DD MMMM, YYYY'));

 // material select initialization
 $('.mdb-select').materialSelect();

// global storage
let cartProducts = []


//*****SELECTORS*****/
// product code form
 const tableForm = document.getElementById("tableForm");
 const tbody = document.getElementById("tbody");
 const error = document.querySelector('#error');


//payment selectors
const formPayment = document.getElementById('formPayment')
const totalPrice = formPayment.querySelector('#totalPrice')
const inputDiscount = formPayment.inputDiscount
const totalPayable = formPayment.querySelector('#totalPayable')
const inputPaid = formPayment.inputPaid
const inputSellingDate = formPayment.inputSellingDate
const totalDue = formPayment.querySelector('#totalDue')
const selectPaymentMethod = formPayment.selectPaymentMethod

//customer
const hiddenCustomerId = formPayment.hiddenCustomerId
const customerError = formPayment.querySelector('#customer-error')

//functions
const localCart = {
    get: function () {
        if (localStorage.getItem('selling-cart-storage')) {
            cartProducts = JSON.parse(localStorage.getItem('selling-cart-storage'));
        }
    },
    set: function () {
        localStorage.setItem('selling-cart-storage', JSON.stringify(cartProducts));
    },
    addToTable: function (product) {
        error.textContent = "";

        const found = cartProducts.some(item => item.ProductId === product.ProductId);
        if (found) {
            error.textContent = `This product already added!`
            return;
        }

        product.SellingQuantity = 0;
        cartProducts.push(product);
        this.set();

        tbody.appendChild(createTableRow(product));
    }
}


//****FUNCTIONS****//
//product autocomplete
 $('#inputProductName').typeahead({
     minLength: 1,
     displayText: function (item) {
         return item.ProductName;
     },
     afterSelect: function (item) {
         this.$element[0].value = ''
     },
     source: function (request, result) {
         $.ajax({
             url: "/Product/FindProductsByName",
             data: { name: request },
             success: function (response) { result(response); },
             error: function (err) { console.log(err) }
         });
     },
     updater: function (item) {
         localCart.addToTable(item);
         return item;
     }
 });

//calculate purchase Total
 const sumTotalPrice = function () {
     const multi = cartProducts.map(item => item.SellingUnitPrice * item.SellingQuantity);
     return multi.reduce((prev, current) => prev + current, 0);
 }

//append total price to DOM
 const appendTotalPrice = function () {
     const totalAmount = sumTotalPrice();

     totalPrice.innerText = totalAmount
     totalPayable.innerText = totalAmount;
     totalDue.innerText = totalAmount;

     if (inputDiscount.value)
         inputDiscount.value = '';

     if (inputPaid.value)
         inputPaid.value = '';

     if (selectPaymentMethod.selectedIndex > 0) {
         clearMDBdropDownList(formPayment);
         selectPaymentMethod.removeAttribute('required');
     }
 }

//create table rows
const createTableRow = function (item) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-id", item.ProductId);

    //column 1
    const td1 = tr.insertCell(0);
    const brand = document.createElement('strong');
    brand.textContent = item.ProductName;

    const product = document.createElement('p');
    product.textContent = item.BrandName;

    td1.appendChild(product);
    td1.appendChild(brand);

    //column 2
    const td2 = tr.insertCell(1);
    const stockSpan = document.createElement('span');
    stockSpan.textContent = item.Stock;

    if (item.Stock < 0) {
        stockSpan.classList.add("red-text");
    }

    td2.appendChild(stockSpan);

    //column 3
    const td3 = tr.insertCell(2);
    const inputQuantity = document.createElement('input');
    inputQuantity.type = "number";
    inputQuantity.step = 0.01;
    inputQuantity.required = true;
    inputQuantity.min = 0;
    inputQuantity.classList.add('form-control', 'inputQuantity');

    if (item.Stock < item.SellingQuantity)
        inputQuantity.classList.add("red-text");

    inputQuantity.value = item.SellingQuantity;
    inputQuantity.setAttribute('data-stock', item.Stock);
    td3.appendChild(inputQuantity);

    //column 4
    const td4 = tr.insertCell(3);
    const inputSellingUnitPrice = document.createElement('input');
    inputSellingUnitPrice.type = "number";
    inputSellingUnitPrice.required = true;
    inputSellingUnitPrice.step = 0.01;
    inputSellingUnitPrice.min = 0;
    inputSellingUnitPrice.classList.add('form-control', 'inputSellingUnitPrice');
    inputSellingUnitPrice.value = item.SellingUnitPrice;
    td4.appendChild(inputSellingUnitPrice);

    //column 5
    const td5 = tr.insertCell(4);
    const inputTotalPrice = document.createElement('span');
    inputTotalPrice.classList.add('inputTotalPrice');
    inputTotalPrice.textContent = item.SellingUnitPrice * item.SellingQuantity;
    td5.appendChild(inputTotalPrice);

    //column 6
    const td6 = tr.insertCell(5);
    const removeIcon = document.createElement('i');
    removeIcon.id = item.ProductId;
    removeIcon.classList.add('fal', 'fa-trash-alt', 'remove');
    td6.appendChild(removeIcon);
    td6.classList.add('text-center');

    return tr;
}

//show product on table
const displayTableData = function () {
    localCart.get();
    appendTotalPrice();

    const fragment = document.createDocumentFragment();

    cartProducts.forEach(item => {
        const tr = createTableRow(item);
        fragment.appendChild(tr);
    });

    tbody.appendChild(fragment);
}

//update product price
const updateProduct = function (productId, field, value) {
    cartProducts.forEach((item, index) => {
        if (item.ProductId === productId) {
            cartProducts[index][field] = value;
            return;
        }
    });

    localCart.set();

    //update total price
    appendTotalPrice();
}

//event listener 
//on remove
tbody.addEventListener('click', function (evt) {
    const element = evt.target;
    const onRemove = element.classList.contains('remove');

    if (onRemove) {
        const id = +element.id;
        cartProducts = cartProducts.filter(item => item.ProductId !== id);
        localCart.set();

        element.parentElement.parentElement.remove();

        //update total price
        appendTotalPrice();
    }
});

//on input value
tbody.addEventListener('input', function (evt) {
    const element = evt.target;
    const row = element.parentElement.parentElement;
    const productId = +element.parentElement.parentElement.getAttribute('data-id');

    const onQuantity = element.classList.contains('inputQuantity');
    const onSellingUnitPrice = element.classList.contains('inputSellingUnitPrice');

    if (onSellingUnitPrice) {
        const quantity = +row.querySelector('.inputQuantity').value;
        const unitPrice = +element.value;
        const totalPrice = row.querySelector('.inputTotalPrice');

        totalPrice.textContent = (quantity * unitPrice).toFixed(2);

        //update value
        updateProduct(productId, "SellingUnitPrice", +element.value);
    }

    if (onQuantity) {
        const sellingUnitPrice = +row.querySelector('.inputSellingUnitPrice').value;
        const quantity = +element.value;
        const totalPrice = row.querySelector('.inputTotalPrice');

        const stock = +element.getAttribute('data-stock');
        if (stock < quantity)
            element.classList.add("red-text");
        else
            element.classList.remove("red-text");

        totalPrice.textContent = (quantity * sellingUnitPrice).toFixed(2);

        //update value
        updateProduct(productId, "SellingQuantity", +element.value);
    }
});

//call function
displayTableData();


//****PAYMENT SECTION****/

//functions
//compare Validation
const validInput = function (total, inputted) {
    return (total < inputted) ? false : true;
}

//input discount amount
const onInputDiscount = function () {
    const total = +totalPrice.textContent;
    const discount = +this.value;
    const isValid = validInput(total, discount);
    const grandTotal = (total - discount);

    this.setAttribute('max', total);

    totalPayable.innerText = isValid ? grandTotal.toFixed() : total;
    totalDue.innerText = isValid ? grandTotal.toFixed() : total;

    if (inputPaid.value)
        inputPaid.value = '';
}

//input paid amount
const onInputPaid = function () {
    const payable = +totalPayable.textContent;
    const paid = +this.value;
    const isValid = validInput(payable, paid);
    const due = (payable - paid);

    paid ? selectPaymentMethod.setAttribute('required', true) : selectPaymentMethod.removeAttribute('required');

    this.setAttribute('max', payable);

    totalDue.innerText = isValid ? due.toFixed() : payable;
}

//reset customer Id
hiddenCustomerId.value = '';

//validation info
const validation = function () {
    customerError.innerText = ''

    if (!cartProducts.length) {
        customerError.innerText = 'Add product to sell!'
        return false;
    }

    const totalAmount = +totalPrice.textContent;
    if (!totalAmount) {
        customerError.innerText = 'Add product to sell!'
        return false;
    }

    if (!hiddenCustomerId.value) {
        customerError.innerText = 'Select or add customer!'
        return false;
    }

    return true;
}

const onCheckFormValid = function (evt) {
    evt.preventDefault()
    tableForm.btnProduct.click()
}

//submit on server
const onSellSubmitClicked = function(evt) {
    evt.preventDefault()

    const valid = validation()
    if (!valid) return;

    //disable button on submit
    const btnSubmit = formPayment.btnSelling
    btnSubmit.innerText = 'submitting..'
    btnSubmit.disabled = true

    const body = {
        CustomerId: +hiddenCustomerId.value,
        SellingTotalPrice: +totalPrice.textContent,
        SellingDiscountAmount: +inputDiscount.value | 0,
        SellingPaidAmount: +inputPaid.value | 0,
        PaymentMethod: inputPaid.value ? selectPaymentMethod.value : '',
        SellingDate: new Date(inputSellingDate.value),
        ProductList: cartProducts
    }

    if (!body.SellingTotalPrice) return;

    const url = '/Selling/Selling'
    const options = {
        method: 'POST',
        url: url,
        data: body
    }

    axios(options).then(response => {
        if (response.data.IsSuccess) {
            localStoreClear()
            location.href = `/Selling/SellingReceipt/${response.data.Data}`
        }
    }).catch(error => {
        if (error.response)
            customerError.textContent = error.response.data.Message;
        else if (error.request)
            console.log(error.request);
        else
            console.log('Error', error.message);
    }).finally(() => {
        btnSubmit.innerText = 'Sell Product';
        btnSubmit.disabled = false;
    });
}

//event listener
formPayment.addEventListener('submit', onCheckFormValid)
tableForm.addEventListener('submit', onSellSubmitClicked)
inputDiscount.addEventListener('input', onInputDiscount)
inputPaid.addEventListener('input', onInputPaid)

//****CUSTOMER****//
//customer autocomplete
$('#inputCustomer').typeahead({
    minLength: 1,
    displayText: function (item) {
        return `${item.CustomerName} ${item.PhonePrimary ? item.PhonePrimary: ''} ${item.OrganizationName ? item.OrganizationName: ''}`;
    },
    afterSelect: function (item) {
        this.$element[0].value = item.CustomerName
    },
    source: function (request, result) {
        $.ajax({
            url: "/Selling/FindCustomers",
            data: { prefix: request },
            success: function (response) { result(response); },
            error: function (err) { console.log(err) }
        });
    },
    updater: function (item) {
        appendInfo(item);
        hiddenCustomerId.value = item.CustomerId;
        customerError.innerText = ''
        return item;
    }
})

function appendInfo(item) {
    const html = `<span class="badge badge-pill badge-success">${item.CustomerName}</span>
        <span class="badge badge-pill badge-info">${item.PhonePrimary}</span>`;

    document.getElementById('customerInfo').innerHTML = html;
}


//remove localstorage
function localStoreClear() {
    localStorage.removeItem('selling-cart-storage');
}
