function UrlShortenFailure(data) {
    $("#UrlDisplay").text(data.responseJSON.response);
    $("#DisplayWrapper").addClass("alert-danger");
    $("#DisplayWrapper").addClass("disabled");
    $("#DisplayWrapper").removeClass("alert-success");
    $("#DisplayWrapper").show();    
}

function UrlShortenSuccess(data) {
    $("#UrlDisplay").text(data.shortenedUrlToken);
    $("#DisplayWrapper").addClass("alert-success");
    $("#DisplayWrapper").removeClass("alert-danger");
    $("#DisplayWrapper").removeClass("disabled");
    $("#DisplayWrapper").show();    
}

$("#UrlDisplay").on("click", function () {
    if (!$("#DisplayWrapper").hasClass("disabled")) {
        copyTextToClipboard($(this).text());
        displayCopiedToClipboardMessage("Shortened Url successfully added to clipboard")
    }
})

function displayCopiedToClipboardMessage(message) {
    var url = $("#UrlDisplay").text();
    $("#UrlDisplay").text(message);
    setTimeout(() => { $("#UrlDisplay").text(url); }, 750); 
}

function copyTextToClipboard(text) {
    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(text);
        return;
    }
    navigator.clipboard.writeText(text).then(function () {
        console.log('Copying to clipboard was successful!');
    }, function (err) {
        console.error('Could not copy text: ', err);
    });
}