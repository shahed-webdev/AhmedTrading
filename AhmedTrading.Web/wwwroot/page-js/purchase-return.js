
//date picker
 $('.datepicker').pickadate().val(moment(new Date()).format('DD MMMM, YYYY'));

 // material select initialization
 $('.mdb-select').materialSelect();

//selectors
//product info form
const tableForm = document.getElementById("tableForm");
const btnTableForm = tableForm.btnTableForm;
const tbody = document.getElementById("tbody");
const error = document.querySelector('#error');

//payment selectors
const formPayment = document.getElementById('formPayment');
const totalPrice = formPayment.querySelector('#totalPrice');
const inputDiscount = formPayment.inputDiscount;
const totalPayable = formPayment.querySelector('#totalPayable');
const inputPaid = formPayment.inputPaid;
const totalDue = formPayment.querySelector('#totalDue');
const selectPaymentMethod = formPayment.selectPaymentMethod;
const inputMemoNumber = formPayment.inputMemoNumber;
const inputPurchaseDate = formPayment.inputPurchaseDate;
const vendorError = formPayment.querySelector('#vendor-error');

let storage = [];

//functions
const localCart = {
    get: function() {
        if (localStorage.getItem('return-cart-storage')) {
            storage = JSON.parse(localStorage.getItem('return-cart-storage'));
        }
    },
    set: function() {
        localStorage.setItem('return-cart-storage', JSON.stringify(storage));
    },
    addToTable: function (product) {
        error.textContent = "";

        const found = storage.some(item => item.ProductId === product.ProductId);
        if (found) {
            error.textContent = `This product already added!`
            return;
        }

        product.PurchaseUnitPrice = 0;
        product.SellingUnitPrice = 0;
        product.PurchaseQuantity = 0;
        storage.push(product);

        this.set();

        tbody.appendChild(createTableRow(product));
    }
}

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
const purchaseTotalPrice = function () {
    const multi = storage.map(item => item.PurchaseUnitPrice * item.PurchaseQuantity);
    return multi.reduce((prev, current) => prev + current, 0);
}

//append total price to DOM
const appendTotalPrice = function () {
    const totalAmount = purchaseTotalPrice();

    totalPrice.innerText = totalAmount
    totalPayable.innerText = totalAmount;
    totalDue.innerText = totalAmount;

    //if (inputDiscount.value)
    //    inputDiscount.value = '';

    //if (inputPaid.value)
    //    inputPaid.value = '';

    //if (selectPaymentMethod.selectedIndex > 0) {
    //    clearMDBdropDownList(formPayment);
    //    selectPaymentMethod.removeAttribute('required');
    //}
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
    const inputSellingUnitPrice = document.createElement('input');
    inputSellingUnitPrice.type = "number";
    inputSellingUnitPrice.required = true;
    inputSellingUnitPrice.step = 0.01;
    inputSellingUnitPrice.min = 0;
    inputSellingUnitPrice.classList.add('form-control', 'inputSellingUnitPrice');
    inputSellingUnitPrice.value = item.SellingUnitPrice;
    td2.appendChild(inputSellingUnitPrice);

    //column 3
    const td3 = tr.insertCell(2);
    const inputQuantity = document.createElement('input');
    inputQuantity.type = "number";
    inputQuantity.required = true;
    inputQuantity.step = 0.01;
    inputQuantity.min = 0;
    inputQuantity.classList.add('form-control', 'inputQuantity');
    inputQuantity.value = item.PurchaseQuantity;
    td3.appendChild(inputQuantity);

    //column 4
    const td4 = tr.insertCell(3);
    const inputUnitPrice = document.createElement('input');
    inputUnitPrice.type = "number";
    inputUnitPrice.required = true;
    inputUnitPrice.step = 0.01;
    inputUnitPrice.min = 0;
    inputUnitPrice.classList.add('form-control', 'inputPurchaseUnitPrice');
    inputUnitPrice.value = item.PurchaseUnitPrice;
    td4.appendChild(inputUnitPrice);

    //column 5
    const td5 = tr.insertCell(4);
    const inputTotalPrice = document.createElement('input');
    inputTotalPrice.type = "number";
    inputTotalPrice.required = true;
    inputTotalPrice.step = 0.01;
    inputTotalPrice.min = 0;
    inputTotalPrice.classList.add('form-control','inputTotalPrice');
    inputTotalPrice.value = item.PurchaseUnitPrice * item.PurchaseQuantity;
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

    storage.forEach(item => {
        const tr = createTableRow(item);
        fragment.appendChild(tr);
    });

    tbody.appendChild(fragment);
}

//update product price
const updateProduct = function(productId, field, value) {
    storage.forEach((item, index) => {
        if (item.ProductId === productId) {
            storage[index][field] = value;
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
        storage = storage.filter(item => item.ProductId !== id);
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
    const onPurchaseUnitPrice = element.classList.contains('inputPurchaseUnitPrice');
    const onTotalPrice = element.classList.contains('inputTotalPrice');
    const onSellingUnitPrice = element.classList.contains('inputSellingUnitPrice');

    if (onSellingUnitPrice) {
        //update value
        updateProduct(productId, "SellingUnitPrice", +element.value);
    }

    if (onQuantity) {
        const purchaseUnitPrice = +row.querySelector('.inputPurchaseUnitPrice').value;
        const quantity = +element.value;
        const totalPrice = row.querySelector('.inputTotalPrice');

        totalPrice.value = (quantity * purchaseUnitPrice).toFixed(2);

        //update value
        updateProduct(productId, "PurchaseQuantity", +element.value);
    }

    if (onPurchaseUnitPrice) {
        const purchaseUnitPrice = +element.value;
        const quantity = +row.querySelector('.inputQuantity').value;
        const totalPrice = row.querySelector('.inputTotalPrice');

        totalPrice.value = (quantity * purchaseUnitPrice).toFixed(2);

        //update value
        updateProduct(productId, "PurchaseUnitPrice", +element.value);
    }

    if (onTotalPrice) {
        const totalPrice = +element.value;
        const quantity = +row.querySelector('.inputQuantity').value;
        const purchaseUnitPrice = row.querySelector('.inputPurchaseUnitPrice');

        const unitPrice = (totalPrice / quantity).toFixed(2);
        purchaseUnitPrice.value = unitPrice;

        //update Quantity
        updateProduct(productId, "PurchaseQuantity", quantity);

        //update UnitPrice
        updateProduct(productId, "PurchaseUnitPrice", unitPrice);
    }
});

//call function
displayTableData();


//****PAYMENT SECTION****/

//functions
//compare Validation
const validInput = function(total, inputted) {
    return (total < inputted) ? false: true;
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

//validation info
const validation = function () {
    vendorError.textContent = ''

    if (!hiddenVendorId.value) {
        vendorError.innerHTML = '<span><i class="fas fa-exclamation-triangle mr-1 red-text"></i>Select or add Vendor!</li>';
        return false;
    }

    if (!storage.length) {
        vendorError.textContent = 'Add product to purchase!';
        return false;
    }
    return true;
}

//remove localstorage
const localStoreClear = function () {
    localStorage.removeItem('return-cart-storage');
}

//check for validation
const onCheckFormValid = function (evt) {
    evt.preventDefault()
    tableForm.btnTableForm.click();
}

//submit on server
const onPurchaseSubmitClicked = function(evt) {
    evt.preventDefault();

    const valid = validation();
    if (!valid) return;

    //disable button on submit
    const btnSubmit = formPayment.btnPurchase;
    btnSubmit.innerText = 'submitting..';
    btnSubmit.disabled = true;

    const body = {
        VendorId: +hiddenVendorId.value,
        PurchaseTotalPrice: +totalPrice.textContent,
        PurchaseDiscountAmount: +inputDiscount.value | 0,
        PurchasePaidAmount: +inputPaid.value | 0,
        PaymentMethod: inputPaid.value ? selectPaymentMethod.value : '',
        MemoNumber: inputMemoNumber.value,
        PurchaseDate: inputPurchaseDate.value,
        Products: storage
    }

    const url = '/Purchase/Purchase';
    $.post(url, { model: body }, function(data) {
        if (data.IsSuccess) {
            localStoreClear();

            btnSubmit.innerText = 'PURCHASE';
            btnSubmit.disabled = false;

            location.href = `/Purchase/PurchaseReceipt/${data.Data}`;
        }
    })
}

//event listener
formPayment.addEventListener('submit', onCheckFormValid);
tableForm.addEventListener('submit', onPurchaseSubmitClicked);
inputDiscount.addEventListener('input', onInputDiscount);
inputPaid.addEventListener('input', onInputPaid);

