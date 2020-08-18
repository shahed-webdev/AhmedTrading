
$(function () {
    $('.datepicker').pickadate();
    dataTable.getData();
});

//selectors
const btnCreate = document.getElementById('CreateClick');
const formPost = document.getElementById("formPost");
const insertModal = $('#InsertModal');

//event listeners
// add expense
btnCreate.addEventListener('click', function (evt) {
    const url = evt.target.getAttribute("data-url");
    axios.get(url).then(res => {
        insertModal.html(res.data).modal('show');
    });
});

//filter by date
formPost.addEventListener('submit', function (evt) {
    evt.preventDefault();

    const inputFromDate = formPost.inputFromDate.value;
    const inputToDate = formPost.inputToDate.value;

    dataTable.filter = [];

    if (inputFromDate)
        dataTable.filter.push({ Field: "ExpenseDate", Value: inputFromDate, Operand: dataTable.operand.GreaterThanOrEqual });

    if (inputToDate)
        dataTable.filter.push({ Field: "ExpenseDate", Value: inputToDate, Operand: dataTable.operand.LessThanOrEqual });

    dataTable.getData();
});

//functions
//create success
function onCreateSuccess(data) {
    if (data !== 'success') return;
    insertModal.modal('hide');
    dataTable.getData();
}


//get data
var dataTable = {
    table: null,
    filter: null,
    init: function () {
        dataTable.table = $("#data-table").DataTable({
            processing: true,
            serverSide: true,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>><"row"<"col-sm-12"<tr>>><"row"<"col-sm-5"i><"col-sm-7"p>>',
            buttons: dataTable.button,
            ajax: {
                url: "/Expenses/IndexData",
                type: "POST",
                data: function (d) {
                    d.filters = dataTable.filter;
                }
            },
            columns:
                [
                    { data: "ExpenseAmount", "render": dataTable.addSign },
                    { data: "ExpenseFor" },
                    { data: "ExpensePaymentMethod" },
                    { data: "ExpenseDate", "render": function (data) { return moment(data).format('DD MMM YYYY') } },
                    { data: "ExpenseId", "render": function (data, type, row, meta) { return `<a class="red-text delete fas fa-trash-alt" href="/Expenses/Delete/${data}"></a>` } }
                ],
            columnDefs: [
                { 'searchable': false, 'targets': [4] },
                { 'sortable': false, 'targets': [4] },
                { 'className': "text-right", "targets": [0] },
                { 'className': "text-left", "targets": [1] }
            ]
        });
    },
    operand: {
        Equal: 0,
        NotEqual: 1,
        GreaterThan: 2,
        LessThan: 3,
        GreaterThanOrEqual: 4,
        LessThanOrEqual: 5,
        Contains: 6,
        StartsWith: 7,
        EndsWith: 8
    },
    getData: function () {
        dataTable.table ? dataTable.table.ajax.reload() : dataTable.init();
    },
    addSign: function (data) { return `${data}/-` },
    button: {
        buttons: [{
            extend: 'print',
            text: '<i class="fa fa-print"></i> Print',
            title: '',
            exportOptions: {
                //columns: [0,1] //Column value those print
            },
            customize: function (win) {
                $(win.document.body).prepend(`<nav class="mb-3 navbar blue-bg">${$('#printBrand').html()}</nav><h3 class="h3-responsive">${$('h4').text()}</h3>`);
            },
            autoPrint: true
        }],
        dom: {
            button: {
                className: 'btn btn-dark btn-rounded btn-sm my-0'
            }
        }
    }
}

//Delete click
$('#data-table').on("click", ".delete", function (evt) {
    evt.preventDefault();

    var row = $(this).closest("tr");
    const url = $(this).attr('href');

    if (!url) return;

    if (confirm("Are you sure you want to delete?")) {
        $.post(url, function (response) {
            row.hide();
        });
    }
});
