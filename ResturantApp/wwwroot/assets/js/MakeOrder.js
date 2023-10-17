$(document).ready(function () {


    var arr = ['1'];
    $("table tbody ").on("change", 'tr td .product', function () {

        var val = $(this).val();

        var selected = $(this).find(":selected").val();
        var rowIndex = $('table tr').index($(this).closest('tr'));
        if (arr.indexOf(selected) != -1) {
            alert("Already selected value");
            $(this).val(arr[rowIndex - 1]);


        } else {

            arr.splice(rowIndex - 1, 1, selected);
        }

        console.log(rowIndex)
        console.log(arr)
    })

    $(".submit").on("click", function () {
        var ids = [];
        var quantity = [];

        $(".product").each(function (e) {
            quantity.push(parseInt($(this).parent().parent().find("input").val()));
            ids.push(parseInt($(this).find("option:selected").val()));
            $.ajax({
                url: '/Order/Submit',
                method: 'post',
                data: { ids: ids, quantity: quantity, customerid: $("#customerid").val(), waiterid: $("#waiterid").find(":selected").val() },
                success: function (Data) {
                    console.log(Data)
                    if (Data.status) {
                        location.href = '/Order/OrderDetails/' + Data.orderid
                    }
                }
            })
            console.log(ids);
            console.log(quantity)
        })

    })


    $("table tbody ").on("click", ' tr td .Removerow', function () {
        var rowIndex = $('table tr').index($(this).closest('tr'));
        arr.splice(rowIndex - 1, 1);
        $(this).closest('tr').remove();
    })
    $("table tbody ").on("click", ' tr td .Addrow', function () {
        $.ajax({
            url: '/Order/Fillselect',
            method: 'Post',
            data: { arr: arr },
            success: function (data) {
                console.log(data);
                var s = '';
                for (var i = 0; i < data.data.length; i++) {
                    s += '<option value="' + data.data[i].id + '">' + data.data[i].name + '</option>';
                    if (i == 0) {
                        arr.push('' + data.data[i].id + '')
                    }
                }

                $("table tbody").append("<tr> <td><select class='product product-last'></select></td><td><input type='number' class='form-control' value='1' min='1'> </td><td  style='width: 150px' class='Action'><a class='btn btn-success Addrow text-light'>+</a><a class='btn btn-danger Removerow text-light'>-</a></td></tr>")
                $(".product-last").html(s);

                $(".product-last").removeClass('product-last');
            }
        })
    })
})