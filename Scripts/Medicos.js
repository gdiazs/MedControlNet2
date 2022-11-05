const Medicos = (function () {

    function _llamarMedicosApi() {
        let response = fetch("/api/medicos").then(response => response.json());
        return response;
    }

    return {
        obtenerMedicos() {
            return _llamarMedicosApi();
        }
    }
});
