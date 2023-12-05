document.addEventListener('mousemove', function(event) {
    event.preventDefault();
}, { passive: false });

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

function getElementPositionWithContainer(elementId, containerElementId) {
    const element = document.getElementById(elementId);
    const container = document.getElementById(containerElementId);
    const rect = element.getBoundingClientRect();
    return {
        top: rect.top + container.scrollTop,
        left: rect.left + container.scrollLeft,
        width: rect.width,
        height: rect.height,
        scrollTop: container.scrollTop,
        scrollLeft: container.scrollLeft
    };
}

function getContainerScrollPosition(containerElementId) {
    const container = document.getElementById(containerElementId);
    return {
        scrollTop: container.scrollTop,
        scrollLeft: container.scrollLeft
    };
}

function setElementPosition(elementId, x, y) {
    const element = document.getElementById(elementId);
    element.style.left = x + "px";
    element.style.top = y + "px";
}

function scrollPage(containerElementId, elementId, scrollSpeed) {
    const container = document.getElementById(containerElementId);

    if(container) {
        container.scrollBy(0, scrollSpeed);
    }
}

function getWindowDimensions() {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
}

function preventArrowKeyScroll(elementId) {
    const element = document.getElementById(elementId);
    element.addEventListener('keydown', function(event) {
        if (["ArrowUp", "ArrowDown", "ArrowLeft", "ArrowRight"].includes(event.key)) {
            event.preventDefault();
        }
    });
}