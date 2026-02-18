function downloadFile(filename, contentType, base64Data) {
    const link = document.createElement('a');
    link.download = filename;
    link.href = `data:${contentType};base64,${base64Data}`;
    link.click();
}
window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = fileName;
    a.click();
    URL.revokeObjectURL(url);
};
