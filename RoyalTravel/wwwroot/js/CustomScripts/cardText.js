const cards = document.getElementsByClassName('card');
const textContext = document.getElementsByClassName('card-img-overlay');

for (let i = 0; i < cards.length; i++) {
    cards[i].addEventListener('mouseenter', function () {
        textContext[i].style.display = 'block';
    });
}