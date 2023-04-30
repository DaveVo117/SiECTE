const MODELO_BASE = {
    idOrganismo: 0,
    txtNombre: "",
    txtSiglas: "",
    txtCorreo: "",
    txtTelefono: "",
    txtTitular: "",
    txtCargoTitular: "",
    txtDireccion:"",
    txtUrlLogo: "",
    snActivo: 1

}//Utilizar inicial minuscula

let tablaData;

$(document).ready(function () {

    $(".card-body").LoadingOverlay("show");

    fetch("/Organismo/Obtener")
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            console.log(responseJson)

            if (responseJson.estado) {
                const d = responseJson.objeto

                $("#txtSiglas").val(d.txtSiglas)
                $("#txtRazonSocial").val(d.txtNombre)
                $("#txtCorreo").val(d.txtCorreo)
                $("#txtDireccion").val(d.txtDireccion)
                $("#txtTelefono").val(d.txtTelefono)
                $("#txtTitular").val(d.txtTitular)
                $("#txtCargoTitular").val(d.txtCargoTitular)
                $("#cboEstado").val(d.snActivo)
                $("#imgLogo").attr("src", d.txtUrlLogo)

            } else {
                swal("Lo sentimos ", responseJson.mensaje, "error")
            }


        });




    dataBind();




})





function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idOrganismo)
    $("#txtNombre").val(modelo.txtNombre)
    $("#txtSiglasModal").val(modelo.txtSiglas)
    $("#txtCorreoModal").val(modelo.txtCorreo)
    $("#txtTelefonoModal").val(modelo.txtTelefono)
    $("#txtTitularModal").val(modelo.txtTitular)
    $("#txtCargoTitularModal").val(modelo.txtCargoTitular)
    $("#txtDireccionModal").val(modelo.txtDireccion)
    $("#cboEstadoModal").val(modelo.snActivo)
    $("#txtLogo").val("")
    $("#logoOrganismo").attr("src", modelo.txtUrlLogo)

    $("#modalData").modal("show")
}

$("#btnNuevo").click(function () {
    mostrarModal()
})





$("#btnGuardarCambios").click(function () {

    //Validación de campos
    const inputs = $("#Principal input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }


//este modelo se envía al controlador para guardar
    const modelo = { 
        txtSiglas: $("#txtSiglas").val(),
        txtNombre: $("#txtRazonSocial").val(),
        txtCorreo: $("#txtCorreo").val(),
        txtDireccion: $("#txtDireccion").val(),
        txtTelefono: $("#txtTelefono").val(),
        txtTitular: $("#txtTitular").val(),
        txtCargoTitular: $("#txtCargoTitular").val(),
        snActivo: $("#cboEstado").val()
        }

    const inputLogo = document.getElementById("txtLogo")

    const formData = new FormData();

    formData.append("logo",inputLogo.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#CardPrincipal .card-body").LoadingOverlay("show");



    fetch("/Organismo/GuardarCambios", {
        method: "POST",
        body:  formData
        })
        .then(response => {
            $("#CardPrincipal .card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado > 0) {
                const d = responseJson.objeto

                $("#imgLogo").attr("src",d.txtUrlLogo)


            } else {
                swal("Lo sentimos ", responseJson.mensaje, "error")
            }


        })

})







//BOTON GUARDAR DE MODAL
$("#btnGuardar").click(function () {

    //validación de campos
    const inputs = $("#Modal input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idOrganismo"] = parseInt($("#txtId").val())
    modelo["txtNombre"] = $("#txtNombre").val();
    modelo["txtSiglas"] = $("#txtSiglasModal").val()
    modelo["nombre"] = $("#txtNombre").val()
    modelo["txtCorreo"] = $("#txtCorreoModal").val()
    modelo["txtTelefono"] = $("#txtTelefonoModal").val()
    modelo["txtTitular"] = $("#txtTitularModal").val()
    modelo["txtCargoTitular"] = $("#txtCargoTitularModal").val()
    modelo["txtDireccion"] = $("#txtDireccionModal").val()
    modelo["txtUrlLogo"] = $("#txtUrlLogoModal").val()
    modelo["snActivo"] = $("#cboEstadoModal").val()

    const inputFoto = document.getElementById("txtLogoModal")

    const formData = new FormData();

    formData.append("foto", inputFoto.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idOrganismo == 0) { /*Inserta nuevo registro*/

        fetch("/Organismo/Crear", {
            method: "POST",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false)
                    $("#modalData").modal("hide")
                    swal("Listo!", "El organismo fue creado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })

    } else {
        //debugger;
        fetch("/Organismo/Editar", {
            method: "PUT",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {
                    tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                    filaSeleccionada = null;
                    $("#modalData").modal("hide")
                    swal("Listo!", "El organismo fue modificado", "success")


                    $('#tbdata').DataTable().destroy();


                    dataBind(); //Función de llenado de tabla


                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })

    }



})






//BOTON EDITAR DE TABLA
let filaSeleccionada;
$("#tbdata tbody").on("click", ".btn-editar", function () {

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();
    //console.log(data)
    mostrarModal(data);
})




//BOTON ELIMINAAR DE TABLA

$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    let fila;

    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "¿Estas seguro?",
        text: `Eliminar al usuario "${data.txtNombre}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (respuesta) {

            if (respuesta) {

                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Organismo/Eliminar?IdOrganismo=${data.idOrganismo}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw();

                            swal("Listo!", "El organismo fue eliminado", "success")
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error")
                        }
                    })


            }

        }
    )

})




//FUNIONES
function dataBind() {



    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Usuario/ListaOrganismosData', /*Para obtener la URL se ejecuta el controlador*/
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idOrganismo", "visible": true, "searchable": false },
            {
                "data": "txtUrlLogo", render: function (data) {
                    return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/ >`
                }
            },
            { "data": "txtSiglas" },
            { "data": "txtNombre" },

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
                filename: 'Reporte Organismos',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6] //especificar columnas que se descargarán inicia en 0
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },





    })


}