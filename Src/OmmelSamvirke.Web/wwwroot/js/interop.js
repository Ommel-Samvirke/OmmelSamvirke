function getElementPosition(elementId) {
    const element = document.getElementById(elementId);
    const rect = element.getBoundingClientRect();
    return {
        top: rect.top,
        left: rect.left,
        width: rect.width,
        height: rect.height
    };
}

function setElementPosition(elementId, x, y) {
    const element = document.getElementById(elementId);
    element.style.left = x + "px";
    element.style.top = y + "px";
}