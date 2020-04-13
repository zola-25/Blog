editPostForm = (function () {

        $(function() {

            $(document).on("click",
                '.admin-delete-post',
                function (e) {
                    $(".admin-delete-post-modal").modal("show");
                });

            $(document).on("click", '.admin-delete-post-confirm', function () {
                $.ajax({
                    url: "/admin/post/" + parseInt($('input[name="PostId"]').val()),
                    method: "DELETE",
                    success: function () {
                        $(".admin-delete-post-modal").modal("hide");
                        $(".admin-delete-post-success-modal").modal("show");
                    }
                });
            });

            $(document).on("click", '.admin-delete-post-success-confirm', function () {

                window.location.replace("/Admin"); // refresh page

            });

            editPostForm.loadTinymce();
        });


        return {
            begin: function () {

                $('.admin-edit-post-result')
                    .empty()
                    .hide()
                    .removeClass("text-success")
                    .removeClass("text-danger");

                return $('#admin-edit-post-form').validate().form();
            },
            success: function (result) {
                $('.admin-edit-post-result').show().addClass("text-success").text(result);
            },

            failure: function (result) {
                $('.admin-edit-post-result').show().addClass("text-warning").text(result.responseText);
            },

            loadTinymce: function () {
                tinymce.remove('#admin-edit-post-html'); 
                tinymce.init({
                    selector: '#admin-edit-post-html',
                    plugins: "preview code"
                });
            }
        };
})();
        
