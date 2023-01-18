const board = document.querySelector('#board')
const SQUARES_NUMBER = 600

for (let i = 0; i < SQUARES_NUMBER; i++) {
    const square = document.createElement('div')
    square.classList.add('square')

    square.addEventListener('mouseover',
        () => setColor(square))

    square.addEventListener('mouseleave',
        () => removeColor(square))

    board.append(square)
}

function getRandomColor() {
    return Math.floor(Math.random() * Math.random()*16777215).toString(16)
}

function setColor(element) {
    const color = getRandomColor()
    element.style.backgroundColor = `#${color}`
    element.style.boxShadow = `0 0 2px #${color}, 0 0 10px #${color}`
}

function removeColor(element) {
    element.style.backgroundColor = '#1d1d1d'
    element.style.boxShadow = '0 0 2px #000'
}