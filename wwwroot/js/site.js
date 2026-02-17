function downloadFile(filename, contentType, base64Data) {
    const link = document.createElement('a');
    link.download = filename;
    link.href = `data:${contentType};base64,${base64Data}`;
    link.click();
}
