

var get_id;
function showmessage1(id) {
    get_id = id;
    $('#del').modal('show');

}

function confirm_delete1() //method 1
{
    window.location.href = "DeleteProducts?id=" + get_id
}




//function confirm_deletes()  //method 2
//{ //ajax
//    $.ajax({
//        url: 'DeleteProducts',
//        type: "get",
//        data: { id: get_id },

//        success: function (result) {
//            window.location.href = "products"
//        }

//    });

//}

// هذا الي تشتغل

function confirm_deletes()  //method 2
{ //ajax
    $.ajax({
        url: 'DeleteProduct',
        type: "get",
        data: { id: get_id },

        success: function (result) {
            const Toast = Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "success",
                title: "  تم الحذف بنجاح "
            });
            $("#productscontainer").html(result)

        }

    });

}


function Advance_search(Name) {

    $.ajax({
        url: 'Advance_search',
        type: 'POST',
        data: { Name: Name },

        success: function (result) {

            $("#productscontainer").html(result)

        }

    });
}