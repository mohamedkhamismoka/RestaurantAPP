
    $(document).ready(function () {
            var dt=$('#dataTable2').DataTable({
        dom: 'Bfrtip',
    lengthMenu: [
    [10, 25, 50, -1],
    ['10 rows', '25 rows', '50 rows', 'Show all']
    ],
    buttons: [
    'pageLength',
    'copyHtml5',
    'colvis',


    {
        extend: 'print',
    title:'Product Menu',
    messageTop: 'Our Product Menu',
    autoPrint: false,
    exportOptions: {
        columns: [0, 1, 2, 3]

                        },
    customize: function (win) {
        $(win.document.body)
            .css({
                'font-size': '10pt',
                'border': '1px solid black'
            })
            .prepend(
                '<img src="http://datatables.net/media/images/logo-fade.png" style="position:absolute; top:0; left:0;" />'
            );

    $(win.document.body).find('table')
    .addClass('compact')

    $(win.document.body).find('thead')

    .css('text-align', 'center');

    $(win.document.body).find('h1').addClass("compacth1")
    $(win.document.body).find('div').addClass("compactdiv")
                        }
                    },
    {
        extend: 'excelHtml5',
    title: 'Product Menu',
    exportOptions: {
        columns: [0, 1, 2, 3]

                        }
                    },
    {
        extend: 'pdfHtml5',
    title:'Product Menu',
    messageTop: function () {
                            return 'Mohamed Restaurant Product Menu';
                        },
    customize:function(doc){
        doc.content[1].alignment = "right";
    //doc.content[2].margin=[150,0,150,0]
    doc.content[2].table.widths =
    Array(doc.content[2].table.body[0].length + 1).join('*').split('');

    var rowCount = doc.content[2].table.body.length;
    for (i = 0; i < rowCount; i++) {
        doc.content[2].table.body[i][0].alignment = 'center';
    doc.content[2].table.body[i][1].alignment = 'center';
    doc.content[2].table.body[i][2].alignment = 'center';
    doc.content[2].table.body[i][3].alignment = 'center';
                            }
    doc.watermark = {text: 'Mohamed Restaurant', color: 'blue', opacity: 0.1 };
                        },


    exportOptions:{
        columns:[0,1,2,3]
                            
                        }
                    }

    ]
            });
    $(".dt-buttons").append("<button id='btn-export'>Word</button>")
    $('body').on('click', '#btn-export', function (e) {
        $.fn.DataTable.Export.word(dt, {
            filename: 'Product Menu',
            title: '<h4 style="text-align:right;margin-top:-150px" >Mohamed Restaurant</h4 > ',
            message: '<h4 style="text-align:right">Product Menu</h4>',
            header: [
                'ID',
                'Name',
                'Description',
                'Price',
              

            ],
            fields: [
                0,
                1,
                2,
               
                3,

            ]
        });
            });
        });
