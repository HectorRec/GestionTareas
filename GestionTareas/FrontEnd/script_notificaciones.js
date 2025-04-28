// Array simulado de tareas programadas (normalmente lo cargarías de una BD)
const tareasDelDia = [
    { nombre: "Terminar tarea de matemáticas", hora: "09:30" },
    { nombre: "Llamar al doctor", hora: "12:00" },
    { nombre: "Comprar material de arte", hora: "16:00" },
    { nombre: "Clase de piano", hora: "18:30" }
];

// Función para cargar las notificaciones
function mostrarNotificaciones() {
    const contenedor = document.getElementById("contenedor_notificaciones");
    contenedor.innerHTML = "";

    tareasDelDia.forEach((tarea) => {
        let div = document.createElement("div");
        div.classList.add("notificacion-item");

        div.innerHTML = `<strong>${tarea.nombre}</strong><br>Hora: ${tarea.hora}`;

        contenedor.appendChild(div);
    });
}

// Opcional: Función para alertar tareas próximas (ejemplo: en menos de 1 hora)
function verificarTareasCercanas() {
    const ahora = new Date();
    const horaActual = ahora.getHours();
    const minutosActuales = ahora.getMinutes();

    tareasDelDia.forEach((tarea) => {
        const [horaTarea, minutoTarea] = tarea.hora.split(":").map(Number);

        const diferenciaMinutos = (horaTarea * 60 + minutoTarea) - (horaActual * 60 + minutosActuales);

        if (diferenciaMinutos > 0 && diferenciaMinutos <= 60) {
            alert(`¡Recuerda! Falta menos de 1 hora para: ${tarea.nombre}`);
        }
    });
}

// Al cargar la página:
mostrarNotificaciones();

// Opcional: Comprobar tareas cercanas cada 5 minutos
setInterval(verificarTareasCercanas, 5 * 60 * 1000);