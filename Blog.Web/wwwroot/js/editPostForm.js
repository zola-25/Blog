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
                    plugins: "preview code codesample link",
                    toolbar: "codesample link",
                    link_assume_external_targets: true,

                    codesample_languages: [
                        {text: 'HTML/XML', value: 'markup'},
                        {text: 'JavaScript', value: 'javascript'},
                        {text: 'CSS', value: 'css'},
                        {text: 'Python', value: 'python'},
                        {text: 'Java', value: 'java'},
                        {text: 'C', value: 'c'},
                        {text: 'C#', value: 'csharp'},
                        {text: 'C++', value: 'cpp'},
                        {text: 'R', value: 'r'},
                        {text: 'SQL', value: 'sql'},
                        {text: 'YAML', value: 'yaml'},
                        {text: 'Docker', value: 'docker'},
                        {text: 'Git', value: 'git'},
                        {text: 'JSON', value: 'json'},
                        {text: 'Excel Formula', value: 'excel-formula'},
                        {text: 'F#', value: 'fsharp'},
                        {text: 'VB.NET', value: 'vbnet'},
                        {text: 'Sass', value: 'scss'},
                        {text: 'Typescript', value: 'typescript'},
                        {text: 'Gherkin', value: 'gherkin'},
                        {text: 'HTTP', value: 'http'},
                        {text: 'ASP.NET', value: 'aspnet'},
                        {text: 'Powershell', value: 'powershell'},
                        {text: 'Bash', value: 'bash'}
                    ]
                });
            }
        };
})();
        
