// Password toggle functionality
document.addEventListener('DOMContentLoaded', function () {
    // For login page
    const passwordInput = document.querySelector('input[type="password"]');
    if (passwordInput) {
        const passwordField = passwordInput.parentElement;
        passwordField.classList.add('password-field');

        const toggle = document.createElement('span');
        toggle.className = 'toggle-password';
        toggle.innerHTML = '👁️';
        toggle.addEventListener('click', function () {
            const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordInput.setAttribute('type', type);
            this.innerHTML = type === 'password' ? '👁️' : '👁️‍🗨️';
        });

        passwordField.appendChild(toggle);
    }

    // For register page - password strength indicator
    const passwordInputs = document.querySelectorAll('input[type="password"]');
    passwordInputs.forEach(input => {
        if (input.id.includes('Password') && !input.id.includes('Confirm')) {
            const strengthBar = document.createElement('div');
            strengthBar.className = 'password-strength';
            strengthBar.innerHTML = '<div class="password-strength-bar"></div>';
            input.parentElement.appendChild(strengthBar);

            input.addEventListener('input', function () {
                const strength = calculatePasswordStrength(this.value);
                const bar = strengthBar.querySelector('.password-strength-bar');
                bar.style.width = `${strength * 25}%`;
                bar.style.backgroundColor = strength < 2 ? 'var(--error-color)' :
                    strength < 4 ? 'orange' : 'var(--success-color)';
            });
        }
    });

    function calculatePasswordStrength(password) {
        let strength = 0;
        if (password.length >= 8) strength++;
        if (/[A-Z]/.test(password)) strength++;
        if (/[0-9]/.test(password)) strength++;
        if (/[^A-Za-z0-9]/.test(password)) strength++;
        return strength;
    }
});