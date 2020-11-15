let activeTitle = document.querySelector('.title_active');
let invisibleForm = document.querySelector('.form_invisible');

const loginRegisterBtns = Array.from(document.querySelectorAll('.login-register__title'));
const forms = Array.from(document.querySelectorAll('.form'));

let link = new Map();
for (let i = 0; i < forms.length; i++)
    link.set(loginRegisterBtns[i], forms[forms.length - 1 - i]);

const classSwapper = (firstObj, secondObj, className) => {
    firstObj.classList.add(className);
    secondObj.classList.remove(className);
    return firstObj;
};

loginRegisterBtns.forEach(elem => elem.addEventListener('click', e => {
    if (!e.target.classList.contains('title_active')) {
        invisibleForm = classSwapper(link.get(e.target), invisibleForm, 'form_invisible');
        activeTitle = classSwapper(e.target, activeTitle, 'title_active');
    }
}));