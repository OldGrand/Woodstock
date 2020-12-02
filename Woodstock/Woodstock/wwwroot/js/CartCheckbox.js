document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('.checkboxInput')
            .forEach(elem => elem.addEventListener("change", OnChangeCheckbox));
});

const OnChangeCheckbox = e => {
    $.ajax({
        type: "POST",
        url: "/ShoppingCart/ChangeSelection",
        data: `watchId=${e.target.previousElementSibling.value}&isChecked=${e.target.checked}`,
        success: function (response) {
            window.location.href = response;
        }
    });
}