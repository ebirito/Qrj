function GridCheckBoxClicked()
{
    EnableDisableUploadVideoButton();
}

function FileUploadChanged()
{
    EnableDisableUploadVideoButton();
}

function EnableDisableUploadVideoButton()
{
    var fileUpload = document.getElementById(ClientIDs.FileUploadClientID);
    var qrCodes = document.getElementById(ClientIDs.QRCodesClientID);
    var uploadVideo = document.getElementById(ClientIDs.UploadVideoClientID);
    var pageIsValid = Page_IsValid;
    if (!Page_IsValid || fileUpload.value == ''){
        uploadVideo.disabled = "disabled";
        return;
    }


    for (var i = 1; i < qrCodes.rows.length; i++) {
        var inputs = qrCodes.rows[i].getElementsByTagName('input');
        if (inputs != null) {
            if (inputs[0].type == "checkbox") {
                if (inputs[0].checked) {
                    uploadVideo.disabled = "";
                    return;
                }
            }
        }
    }

    uploadVideo.disabled = "disabled";
}