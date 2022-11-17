const Api = (function () {

    function _llamarMedicosApi() {
        let response = fetch("/api/medicos").then(response => response.json());
        return response;
    }

    function _obtenerEspecialidadesApi() {
        let response = fetch("/api/especialidades").then(response => response.json());
        return response;
    }

    function _llamarGuardarMedicoApi (nuevoMedico) {
        let response = fetch("/api/medicos", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevoMedico)
        }).then(response => {
            if (!response.ok) {
                return response.json().then(data => { throw data })
            }
            return response;

        }).then(response => response.json());

        return response;
    }

    return {
        obtenerMedicos() {
            return _llamarMedicosApi();
        },
        agregarMedico(nuevoMedico) {
            return _llamarGuardarMedicoApi(nuevoMedico);
        },

        obtenerEspecialidades() {
            return _obtenerEspecialidadesApi();
        }

    }
});
