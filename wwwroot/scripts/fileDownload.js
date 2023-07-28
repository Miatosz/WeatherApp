window.saveAsFile = function (fileName, bytesBase64) {
    const link = document.createElement('a');
    link.download = fileName;
    link.href = "data:text/csv;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};