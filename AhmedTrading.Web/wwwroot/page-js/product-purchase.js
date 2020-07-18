
//date picker
 $('.datepicker').pickadate().val(moment(new Date()).format('DD MMMM, YYYY'));

//global store
let storage = [];

//selectors
//product info form
const formCart = document.getElementById("cart-form")

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


//functions
const getStorage = function () {
    if (localStorage.getItem('cart-storage')) {
        storage = JSON.parse(localStorage.getItem('cart-storage'));
    };
}

//vendor autocomplete
$('#inputProductName').typeahead({
    minLength: 1,
    displayText: function (item) {
        return item.ProductName;
    },
    afterSelect: function (item) {
        this.$element[0].value = item.ProductName
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
        console.log(item)
        return item;
    }
});


//calculate purchase Total
const purchaseTotalPrice = function () {
    const multi = storage.map(item => item.PurchasePrice * item.ProductStocks.length);
    return multi.reduce((prev, current) => prev + current, 0);
}

//append total price to DOM
const appendTotalPrice = function () {
    const totalAmount = purchaseTotalPrice();

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
    tr.setAttribute('data-sn', item.SN);

    //column 1
    const td1 = tr.insertCell(0);
    td1.appendChild(document.createTextNode(item.Category));

    const p = document.createElement('p');
    p.textContent = item.Description;
    td1.appendChild(p);

    //column 2
    const td2 = tr.insertCell(1);
    td2.appendChild(document.createTextNode(item.ProductName));

    const p2 = document.createElement('p');
    p2.textContent = item.Note;
    td2.appendChild(p2);

    //column 3
    const td3 = tr.insertCell(2);
    td3.appendChild(document.createTextNode(item.PurchasePrice));

    //column 4
    const td4 = tr.insertCell(3);
    td4.appendChild(document.createTextNode(item.SellingPrice));

    //column 5
    const td5 = tr.insertCell(4);
    td5.appendChild(document.createTextNode(item.Warranty));

    //column 6
    const td6 = tr.insertCell(5);
    const strong = document.createElement('strong');
    strong.appendChild(document.createTextNode(item.ProductStocks.length));
    strong.classList.add('badge-pill', 'badge-success', 'stock');
    td6.appendChild(strong);

    //column 6
    const td7 = tr.insertCell(6);
    const removeIcon = document.createElement('i');
    removeIcon.classList.add('fal', 'fa-trash-alt', 'remove');
    td7.appendChild(removeIcon);
    td7.classList.add('text-center');

    return tr;
}

//show product on table
const showCartedProduct = function () {
    getStorage();
    appendTotalPrice();

    const fragment = document.createDocumentFragment();

    storage.forEach((item ,SN) => {
        const tr = createTableRow(item, SN+1);
        fragment.appendChild(tr);
    });

    tbody.appendChild(fragment);
 }

//call function
showCartedProduct();



//****VENDORS****//

//selectors
const vendorAddClick = document.getElementById('vendorAddClick');
const inputFindVendor = document.getElementById('inputFindVendor');
const vendorInfo = document.getElementById('vendor-info');
const hiddenVendorId = document.getElementById('vendorId');
const insertModal = $('#InsertModal');

//functions

//get vendor insert modal
const onVendorAddClicked = function () {
    const url = this.getAttribute('data-url');

    axios.get(url)
        .then(response => insertModal.html(response.data).modal('show'))
        .catch(err => console.log(err))
}

//append vendor info to DOM
const appendVendorInfo = function (data) {
    hiddenVendorId.value = data.VendorId;
    vendorInfo.innerHTML = '';

    const html = `
        <li class="list-group-item"><i class="fas fa-building"></i> ${data.VendorCompanyName}</li>
        <li class="list-group-item"><i class="fas fa-user-tie"></i> ${data.VendorName}</li>
        <li class="list-group-item"><i class="fas fa-phone"></i> ${data.VendorPhone}</li>
        <li class="list-group-item"><i class="fas fa-map-marker-alt"></i> ${data.VendorAddress}</li>`;

    vendorInfo.innerHTML= html;
}

//vendor create success
function onCreateSuccess(response) {
    if (response.Status) {
        insertModal.modal('hide');
        inputFindVendor.value = '';

        appendVendorInfo(response.Data);
    }
    else {
        insertModal.html(response);
    }
}

//reset vendorId
hiddenVendorId.value = ''

//vendor autocomplete
$('#inputFindVendor').typeahead({
    minLength: 1,
    displayText: function (item) {
        return `${item.VendorCompanyName} (${item.VendorName}, ${item.VendorPhone})`;
    },
    afterSelect: function (item) {
        this.$element[0].value = item.VendorCompanyName
    },
    source: function (request, result) {
        $.ajax({
            url: "/Purchase/FindVendor",
            data: { prefix: request },
            success: function (response) { result(response); },
            error: function (err) { console.log(err) }
        });
    },
    updater: function (item) {
        appendVendorInfo(item);
        return item;
    }
});

//event listener
vendorAddClick.addEventListener('click', onVendorAddClicked);


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
        vendorInfo.innerHTML = '<li class="list-group-item list-group-item-danger text-center"><i class="fas fa-exclamation-triangle mr-1 red-text"></i>Select or add Vendor for Purchase!</li>';
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
    localStorage.removeItem('cart-storage');
}

//submit on server
const onPurchaseSubmitClicked = function(evt) {
    evt.preventDefault();

    const valid = validation();
    if (!valid) return;

    //disable button on submit
    const btnSubmit = evt.target.btnPurchase;
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
formPayment.addEventListener('submit', onPurchaseSubmitClicked);
inputDiscount.addEventListener('input', onInputDiscount);
inputPaid.addEventListener('input', onInputPaid);

