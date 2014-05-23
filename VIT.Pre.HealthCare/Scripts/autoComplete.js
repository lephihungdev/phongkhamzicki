/// <reference path="jquery-1.6.2-vsdoc.js" />
/// <reference path="jquery-ui.js" />
$(document).ready(function () {
    $('*[data-autocomplete-url]')
        .each(function () {
            var $this = $(this);
            $this.autocomplete({
                change: function(event, ui) {
                    
                    // blank if nothing was selected
                    if (!ui.item) {
                        $this.val(null);

                        // get auto complete id 
                        // set value for hidden field
                        var id = $this.attr("data-autocomplete-id");
                        var $hidden = $("[type='hidden'][data-autocomplete-id='" + id + "']");
                        $hidden.val("-1");
                    }
                },
                select: function (event, ui) {
                    // display text 
                    event.preventDefault();
                    $this.val(ui.item.label);

                    // get auto complete id 
                    // set value for hidden field
                    var id = $this.attr("data-autocomplete-id");
                    var $hidden = $("[type='hidden'][data-autocomplete-id='" + id + "']");
                    $hidden.val(ui.item.value);
                },

                source: $(this).data("autocomplete-url")
            });

            // check if hidden field has value
            // get display name by hidden id
            // set display name for textbox autocomplete
            var id = $this.attr("data-autocomplete-id");
            var $hidden = $("[type='hidden'][data-autocomplete-id='" + id + "']");
            if ($hidden.val() != "-1" || $hidden.val() != "" || $hidden.val() != null) {
                var value = parseInt($hidden.val());
                if (value > 0) {
                    var getTextUrl = $(this).attr("data-autocomplete-getdisplaytext_url");
                    if (getTextUrl != null || getTextUrl != "") {
                        $.ajax({
                            url: getTextUrl,
                            type: 'POST',
                            data: { value: value },
                            success: function (data) {
                                $this.val(data.label);
                            },
                            error: function (xhr) { alert("Something seems Wrong"); }
                        });
                    }
                }
            }
        });
});