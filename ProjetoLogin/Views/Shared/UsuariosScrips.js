let allowLeave = false;

// Libera saída apenas se clicar no botão "Salvar"
document.getElementById("btnSalvar")?.addEventListener("click", function () {
    allowLeave = true;
});

// Intercepta qualquer tentativa de saída (reload, fechar aba, navegar etc.)
window.addEventListener("beforeunload", function (e) {
    if (!allowLeave) {
        e.preventDefault();
        e.returnValue = '';
    }
});

// Intercepta cliques em links e botões que mudam a página
document.addEventListener("click", function (e) {
    if (!allowLeave) {
        let target = e.target;

        // Sobe até o <a> ou <button> se for filho
        while (target && !target.href && target.tagName !== 'BUTTON') {
            target = target.parentElement;
        }

        if (target && (target.tagName === 'A' || target.tagName === 'BUTTON')) {
            if (!target.closest('#btnSalvar')) {
                e.preventDefault();
                alert("Por favor, Complete o Cadastro antes de sair desta página.");
            }
        }
    }
});

// Intercepta mudança de rota no botão 'Voltar' do navegador
window.addEventListener("popstate", function (e) {
    if (!allowLeave) {
        history.pushState(null, '', location.href); // volta a URL para onde estava
        alert("Por favor, Complete o Cadastro antes de sair desta página.");
    }
});

// Hack: bloqueia o botão voltar mantendo o histórico sempre com a mesma entrada
window.onload = function () {
    history.pushState(null, '', location.href);
};