const mainCheck = document.querySelector('#mainCheck');

const childCheckbox = document.querySelectorAll('.cart-checkbox');

mainCheck.addEventListener('change', e => {
    if (e.target.checked)
        childCheckbox.forEach(e => e.checked = true);
    else
        childCheckbox.forEach(e => e.checked = false);
});