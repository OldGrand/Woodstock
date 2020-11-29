document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('.checkboxInput')
            .forEach(elem => elem.addEventListener("change", OnChangeCheckbox));
});

const OnChangeCheckbox = e => {
    console.log(e.target.form.submit());
    $.ajax({
        type: "POST",
        url: "/ShoppingCart/ChangeSelection",
        data: `watchId=${e.target.previousElementSibling.value}&isChecked=${e.target.checked}`,
        success: function (response) {
            window.location.href = response;
            window.reload();
        }
    });
}