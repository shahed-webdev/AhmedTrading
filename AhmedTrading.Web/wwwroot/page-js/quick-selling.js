
 // material select initialization
 $('.mdb-select').materialSelect();

// global storage
 let cartProducts = [];


//*****SELECTORS*****/
// product code form
 const tableForm = document.getElementById("tableForm");
 const tbody = document.getElementById("tbody");
 const error = document.querySelector('#error');

//payment selectors
 const formPayment = document.getElementById('formPayment');
 const totalPrice = formPayment.querySelector('#totalPrice');
 const inputDiscount = formPayment.inputDiscount;
 const totalPayable = formPayment.querySelector('#totalPayable');
 const selectPaymentMethod = formPayment.selectPaymentMethod;

//customer
 const hiddenCustomerId = formPayment.hiddenCustomerId;
 const customerError = formPayment.querySelector('#customer-error');

//functions
const localCart = {
    get: function () {
        if (localStorage.getItem('quick-cart-storage')) {
            cartProducts = JSON.parse(localStorage.getItem('quick-cart-storage'));
        }
    },
    set: function () {
        localStorage.setItem('quick-cart-storage', JSON.stringify(cartProducts));
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

     if (inputDiscount.value)
         inputDiscount.value = '';
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
    inputQuantity.required = true;
    inputQuantity.min = 1;
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
    inputSellingUnitPrice.min = 1;
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
}

//validation info
const validation = function () {
    customerError.innerText = ''

    if (!cartProducts.length) {
        customerError.innerText = 'Add product to sell!'
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
        CustomerId: null,
        SellingTotalPrice: +totalPrice.textContent,
        SellingDiscountAmount: +inputDiscount.value || 0,
        SellingPaidAmount: +totalPayable.innerText || 0,
        PaymentMethod: selectPaymentMethod.value,
        SellingDate: new Date(),
        ProductList: cartProducts
    }

    const url = '/Selling/QuickSelling'
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
 formPayment.addEventListener('submit', onCheckFormValid);
 tableForm.addEventListener('submit', onSellSubmitClicked);
 inputDiscount.addEventListener('input', onInputDiscount);


//remove localstorage
function localStoreClear() {
    localStorage.removeItem('quick-cart-storage');
}
