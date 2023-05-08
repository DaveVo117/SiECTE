let valorImpuesto;


$(document).ready(function () {

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaIngreso").datepicker({ dateFormat: "dd/mm/yy" });
    $("#txtFechaNacimientoB").datepicker({ dateFormat: "dd/mm/yy" });

    fetch('/Residente/ListaCatDocumentos')
        .then(response => response.json())
        .then(data => {
            const checkboxContainer = document.getElementById('checkbox-container');
            data.forEach(item => {
                const checkbox = document.createElement('input');
                checkbox.type = 'checkbox';
                checkbox.classList.add('checkbox-with-padding');
                checkbox.name = item.txtDocumentoIngreso; // aquí puedes poner el nombre del item como quieras
                checkbox.value = item.idDocumentoIngreso; // aquí puedes poner el valor del item como quieras
                checkbox.title = item.txtDescripcion; // aquí estableces la descripción del checkbox

                const label = document.createElement('label');
                label.classList.add('label-block');
                label.appendChild(checkbox);
                label.appendChild(document.createTextNode(item.txtDocumentoIngreso));

                const div = document.createElement('div');
                div.appendChild(label);

                checkboxContainer.appendChild(div);
            });
        });



    //Acordeon
    //Acordeon
    //fetch('/Residente/ListaTipoNotas')
    //    .then(response => response.json())
    //    .then(data => {
    //        const accordion = document.getElementById('accordion');
    //        data.forEach(item => {
    //            const sectionHeader = document.createElement('h3');
    //            sectionHeader.textContent = item.txtTipoNota;

    //            const sectionContent = document.createElement('div');

    //            const section = document.createElement('div');
    //            section.appendChild(sectionHeader);
    //            section.appendChild(sectionContent);

    //            const txtNota = document.createElement('input');
    //            txtNota.setAttribute("type", "text");
    //            txtNota.setAttribute("id", "txtNota");
    //            sectionContent.appendChild(txtNota);

    //            accordion.appendChild(section);
    //        });

    //        $("#accordion").accordion();
    //    });







})

{ dateFormat: "dd/mm/yy" }





let tablaData;

$("#btnBuscarB").click(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": `/SIEB/SiebBusquedaIdBeneficiario?id="${txtBIDBeneficiario.value}"`, /*Para obtener la URL se ejecuta el proyecto*/
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "visible": false, "searchable": false },
            { "data": "nombreCompleto" },
            { "data": "txtCorreo" },
            { "data": "txtTelefono" },
            { "data": "txtRol" },
            {
                "data": "fechaAlta", render: function (data) {
                    var fecha = new Date(data);
                    var dia = ("0" + fecha.getDate()).slice(-2);
                    var mes = ("0" + (fecha.getMonth() + 1)).slice(-2);
                    var anio = fecha.getFullYear().toString().substr(-2);
                    return dia + "/" + mes + "/" + anio;
                }
            },
            {
                "data": "snActivo", render: function (data) {
                    if (data == 1)
                        return '<pan class="badge badge-info">Activo </span>';
                    else
                        return '<pan class="badge badge-danger">No Activo </span>';

                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Usuarios',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6] //especificar columnas que se descargarán inicia en 0
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });


})





//$JQuery, id de tabla de la vista Index
//$(document).ready(function () {


//    $.datepicker.setDefaults($.datepicker.regional["es"])

//    $("#txtFechaNacimiento").datepicker({ dateFormat: "dd/mm/yy" })


//    fetch("/Venta/ListaTipoDocumentoVenta")
//        .then(response => { 
//            return response.ok ? response.json() : Promise.reject(response);
//        })
//        .then(responseJson => {
//            if (responseJson.length > 0) {
//                responseJson.forEach((item) => {
//                    $("#cboTipoDocumentoVenta").append(
//                        $("<option>").val(item.idTipoDocumentoVenta).text(item.txtDescripcion)
//                        )
//                })
//            }
//        })



//    fetch("/Negocio/Obtener")
//        .then(response => {
//            return response.ok ? response.json() : Promise.reject(response);
//        })
//        .then(responseJson => {

//            if (responseJson.estado) {
//                const d = responseJson.objeto;
//                console.log(d)

//                $("#inputGroupSubTotal").text(`Sub Total-${d.txtSimboloMoneda}`)
//                $("#inputGroupIGV").text(`IGV(${d.porcentajeImpuesto})-${d.txtSimboloMoneda}`)
//                $("#inputGroupTotal").text(`Total-${d.txtSimboloMoneda}`)

//                valorImpuesto = parseFloat(d.porcentajeImpuesto)
//            }

//        })



//    $("#cboBuscarProducto").select2({
//        ajax: {
//            url: "/Venta/ObtenerProductos",
//            dataType: 'json',

//            contentType: "application/json; charset=utf-8",

//            delay: 250,
//            data: function (params) {
//                return {
//                 busqueda: params.term /*parametro busqueda del controller*/  
//                };
//            },
//            processResults: function (data,) {

//                return {
//                    results: data.map((item) => (
//                        {
//                            id: item.idProducto,
//                            text: item.txtDescripcion,

//                            marca: item.txtMarca,
//                            categoria: item.txtNombreCategoria,
//                            urlImagen: item.txtUrlImagen,
//                            precio: parseFloat(item.precio)

//                        }
//                    ))

//                };
//            },

//        },
//        language:"es",
//        placeholder: 'Buscar Producto...',
//        minimumInputLength: 1,
//        templateResult: formatoResultados
//    });





//})


//function formatoResultados(data) {
//    //esto es por defecto ya que muestra el loading...
//    if (data.loading) {
//        return data.text;
//    }

//    var contenedor = $(
//        `<table width="100%">
//                <tr>
//                    <td style="width:60px">
//                        <img style="height:60px; width:60px; margin-right:10px" src="${data.urlImagen}">
//                    </td>
//                    <td>
//                        <p style="font-weight:bolder; margin:2px">${data.marca}</p>
//                        <p style="margin:2px">${data.text}</p>
//                    </td>
//                </tr>
//             </table>
//             `
//    );
//    return contenedor;

//}



//Hace que al dar clic en el cbo de busqueda de producto el cursor quede dentro del input de busqueda(focus)
//$(document).on("select2:open", function () {
//    document.querySelector(".select2-search__field").focus();
//})





//let productosParaVenta = [];

//$("#cboBuscarProducto").on("select2:select", function (e) {
//    const data = e.params.data;
//    /*console.log(data)*/
//    let producto_encontrado = productosParaVenta.filter(p=>p.idProducto == data.id)
//    if (producto_encontrado.length > 0) {
//        $("#cboBuscarProducto").val("").trigger("change")
//        toastr.warning("", "El producto ya fue agregado")
//        return false
//    }


//    swal({
//        title: data.marca,
//        text: data.text,
//        imageUrl: data.urlImagen,
//        type:"input",
//        showCancelButton: true,
//        closeOnConfirm: true,
//        inputPlaceholder:"Ingrese Cantidad"
//    },
//        function (valor) {

//            if (valor === false) return false;

//            if (valor === "") {
//                toastr.warning("", "Necesita ingresar la cantidad")
//                return false;
//            }

//            if (isNaN(parseInt(valor))) {
//                toastr.warning("", "Debe ingresar un valor numérico")
//                return false;
//            }

//            let producto = {
//                idProducto: data.id,
//                txtMarcaProducto: data.marca,
//                txtDescripcionProducto: data.text,
//                txtCategoriaProducto: data.categoria,
//                cantidad: parseInt(valor),
//                precio: data.precio.toString(),
//                total: (parseFloat(valor) * data.precio).toString()
//                }

//            productosParaVenta.push(producto)
//            MostrarProducto_Precio();

//            $("#cboBuscarProducto").val("").trigger("change")
//            swal.close()
//        }
//    )


//})






//function MostrarProducto_Precio() {

//    let total = 0;
//    let igv = 0;
//    let subtotal = 0;
//    let porcentaje = valorImpuesto / 100;

//    $("#tbProducto tbody").html("")

//    productosParaVenta.forEach((item) => {

//        total = total + parseFloat(item.total)

//        $("#tbProducto tbody").append(
//            $("<tr>").append(
//                $("<td>").append($("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append($("<i>").addClass("fas fa-trash-alt")).data("idProducto",item.idProducto)),
//                $("<td>").text(item.txtDescripcionProducto),
//                $(`<td> style="display:flex; align-content:center;"`).text(item.cantidad),
//                $("<td>").text(item.precio),
//                $("<td>").text(item.total)
//                )
//            )

//    })

//    subtotal = total / (1 + porcentaje);
//    igv = total - subtotal;

//    $("#txtSubTotal").val(subtotal.toFixed(2))
//    $("#txtIGV").val(igv.toFixed(2))
//        $("#txtTotal").val(total.toFixed(2))

//}





//$(document).on("click","button.btn-eliminar", function () {

//    const _idProducto = $(this).data("idProducto")

//    productosParaVenta = productosParaVenta.filter(p => p.idProducto != _idProducto);

//    MostrarProducto_Precio();

//})





//$("#btnTerminarVenta").click(function () {

//    if (productosParaVenta.length < 1) {
//        toastr.warning("", "Debe ingresar al menos un producto")
//        return;
//    }

//    const vmDetalleVenta = productosParaVenta;

//    const venta = {
//        idTipoDocumentoVenta: $("#cboTipoDocumentoVenta").val(),
//        txtDocumentoCliente: $("#txtDocumentoCliente").val(),
//        txtNombreCliente: $("#txtNombreCliente").val(),
//        subTotal: $("#txtSubTotal").val(),
//        impuestoTotal: $("#txtIGV").val(),
//        total: $("#txtTotal").val(),
//        detalleVenta: vmDetalleVenta
//    }

//    $("#btnTerminarVenta").LoadingOverlay("show")

//    fetch("/Venta/RegistrarVenta", {
//        method: "POST",
//        headers: { "Content-Type": "application/json; charset=utf-8" },
//        body: JSON.stringify(venta)
//    })

//        .then(response => {
//            $("#btnTerminarVenta").LoadingOverlay("hide")
//            return response.ok ? response.json() : Promise.reject(response);
//        })
//        .then(responseJson => {

//            if (responseJson.estado) {
//                productosParaVenta = [];
//                MostrarProducto_Precio();

//                $("#txtDocumentoCliente").val("")
//                $("#txtNombreCliente").val("")
//                $("#cboTipoDocumentoVenta").val($("#cboTipoDocumentoVenta option:first").val())
//                swal("Registrado!", `Número Venta: ${responseJson.objeto.txtNumeroVenta}`, "success")

//            } else {

//                swal("Lo sentimos!", `No se pudo cargar la venta`, "error")

//            }

//        })

//})