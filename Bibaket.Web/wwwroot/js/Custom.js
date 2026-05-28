/*Start RePasword Js */
// password-form.js
document.querySelectorAll('.password-toggle').forEach(toggle => {
    toggle.addEventListener('click', function () {
        const input = this.parentElement.querySelector('.field-input');
        const type = input.getAttribute('type') === 'password' ? 'text' : 'password';
        input.setAttribute('type', type);
        this.classList.toggle('fa-eye');
        this.classList.toggle('fa-eye-slash');
    });
});
///*end RePasword Js */

///*Start Register Js */
//// نمایش/عدم نمایش رمز عبور
//document.querySelectorAll('.password-toggle').forEach(toggle => {
//    toggle.addEventListener('click', function () {
//        const input = this.parentElement.querySelector('.field-input');
//        const type = input.getAttribute('type') === 'password' ? 'text' : 'password';
//        input.setAttribute('type', type);
//        this.classList.toggle('fa-eye');
//        this.classList.toggle('fa-eye-slash');
//    });
//});
///*end Register Js */
//// Custom.js - نسخه ساده و مطمئن