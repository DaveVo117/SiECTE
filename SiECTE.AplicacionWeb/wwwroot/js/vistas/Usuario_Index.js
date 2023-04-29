const MODELO_BASE={
    idUsuario: 0,
    nombreCompleto: "",
    cveUsuario: "",
    nombre: "",
    primerApellido: "",
    segundoApellido: "",
    txtCorreo: "",
    txtTelefono: "",
    cargo: "",
    noEmpleado: "",
    idRol: 0,
    idOrganismo: 0,
    snActivo: 1,
    txtUrlFoto: ""

}//Utilizar inicial minuscula

let tablaData;

//$JQuery, id de tabla de la vista Index
$(document).ready(function () {

    fetch("/Usuario/ListaRoles")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboRol").append(
                        $("<option>").val(item.idRol).text(item.txtRol)
                    )
                })
            }
        });

    fetch("/Usuario/ListaOrganismos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboOrganismo").append(
                        $("<option>").val(item.idOrganismo).text(item.txtNombre)
                    )
                })
            }
        });


    tablaData =   $('#tbdata').DataTable({
        responsive: true,
         "ajax": {
             "url": '/Usuario/Lista', /*Para obtener la URL se ejecuta el proyecto*/
             "type": "GET",
             "datatype": "json"
         },
         "columns": [
             { "data": "idUsuario" ,"visible":false,"searchable":false},
             {
                 "data": "txtUrlFoto", render: function (data) {
                    return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/ >`
                 }
             },
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
                    columns: [2,3,4,5,6] //especificar columnas que se descargarán inicia en 0
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });



    //Esconde observaciones
    $("#txtObservacionBaja").hide();
    $("#lblObservacionBaja").hide();

    $("#cboEstado").change(function () {
        if ($(this).val() == 1 ) {
            $("#txtObservacionBaja").hide();
            $("#lblObservacionBaja").hide();
        } else {
            $("#txtObservacionBaja").show();
            $("#lblObservacionBaja").show();
        }
    });


    //inputs a mayusculas
    $('input[type="text"]').on('input', function () {
        this.value = this.value.toUpperCase();
    });



    // captura numerica
    var inputNoEmpleado = $("#txtNoEmpleado");

    // Agregamos el evento keypress
    inputNoEmpleado.keypress(function (event) {
        // Obtenemos el código de la tecla presionada
        var charCode = event.which;

        // Validamos que sea un número
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            // Si no es un número, cancelamos el evento keypress
            event.preventDefault();
        }
    });


    // captura numerica
    var inputTel = $("#txtTelefono");

    // Agregamos el evento keypress
    inputTel.keypress(function (event) {
        // Obtenemos el código de la tecla presionada
        var charCode = event.which;

        // Validamos que sea un número
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            // Si no es un número, cancelamos el evento keypress
            event.preventDefault();
        }
    });






})


function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idUsuario)
    $("#txtCveUsuario").val(modelo.cveUsuario)
    $("#txtNombre").val(modelo.nombre)
    $("#txtPrimerApellido").val(modelo.primerApellido)
    $("#txtSegundoApellido").val(modelo.segundoApellido)
    $("#txtCorreo").val(modelo.txtCorreo)
    $("#txtTelefono").val(modelo.txtTelefono)
    $("#cboRol").val(modelo.idRol == 0 ? $("#cboRol option:first").val() : modelo.idRol)
    $("#txtCargo").val(modelo.cargo)
    $("#txtNoEmpleado").val(modelo.noEmpleado)
    $("#cboEstado").val(modelo.snActivo)
    $("#cboOrganismo").val(modelo.idOrganismo== 0 ? $("#cboOrganismo option:first").val() : modelo.idOrganismo)
    $("#txtFoto").val("")
    $("#imgUsuario").attr("src",modelo.txtUrlFoto)

    $("#modalData").modal("show")
}

$("#btnNuevo").click(function (){
    mostrarModal()
})

$("#btnGuardar").click(function () {

    //validación de campos
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idUsuario"] = parseInt($("#txtId").val())
    modelo["nombreCompleto"] = $("#txtNombre").val() + " " + $("#txtPrimerApellido").val() + " " + $("#txtSegundoApellido").val();
    modelo["cveUsuario"] = $("#txtCveUsuario").val()
    modelo["nombre"] = $("#txtNombre").val()
    modelo["primerApellido"] = $("#txtPrimerApellido").val()
    modelo["segundoApellido"] = $("#txtSegundoApellido").val()
    modelo["TxtCorreo"] = $("#txtCorreo").val()
    modelo["txtTelefono"] = $("#txtTelefono").val()
    modelo["idRol"] = $("#cboRol").val()
    modelo["idOrganismo"] = $("#cboOrganismo").val()
    modelo["cargo"] = $("#txtCargo").val()
    modelo["noEmpleado"] = $("#txtNoEmpleado").val()
    modelo["snActivo"] = $("#cboEstado").val()

    const inputFoto = document.getElementById("txtFoto")

    const formData = new FormData();

    formData.append("foto", inputFoto.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idUsuario == 0) { /*Inserta nuevo registro*/

        fetch("/Usuario/Crear", {
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
                    swal("Listo!", "El usuario fue creado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })

    } else {
        //debugger;
        fetch("/Usuario/Editar", {
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
                    swal("Listo!", "El usuario fue modificado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })

    }



})


let filaSeleccionada;
$("#tbdata tbody").on("click", ".btn-editar", function () {

    if($(this).closest("tr").hasClass("child")){
        filaSeleccionada = $(this).closest("tr").prev();
    }else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();
    //console.log(data)
    mostrarModal(data);
})


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

                fetch(`/Usuario/Eliminar?IdUsuario=${data.idUsuario}`, {
                    method: "DELETE"
                })
                .then(response => {
                     $(".showSweetAlert").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                })
                .then(responseJson => {

                if (responseJson.estado) {

                    tablaData.row(fila).remove().draw();

                    swal("Listo!", "El usuario fue eliminado", "success")
                 } else {
                        swal("Lo sentimos", responseJson.mensaje, "error")
                    }
                })


            }

        }
    )

})