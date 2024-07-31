document.addEventListener('DOMContentLoaded', () => {
    
    
    const telInputs = document.querySelectorAll('input[type=tel]');
    phoneMask(telInputs);

    modalsInit();

    const logoutButton = document.querySelector('.logout');
    logoutButton.addEventListener('click', () => {
        localStorage.removeItem('token');
        console.log(localStorage);
        window.location.href = '/user/login';
    });
});
