// Este array guardará tareas con su hora
let tareasConHora = [];

// Función para agregar una tarea con hora
function agregarTareaConHora() {
  let nombreCampo = document.getElementById("tarea_horario");
  let horaCampo = document.getElementById("hora_horario");

  let nombreTarea = nombreCampo.value.trim();
  let horaTarea = horaCampo.value.trim();

  if (nombreTarea === "" || horaTarea === "") {
    alert("Debes escribir el nombre de la tarea y la hora.");
    return;
  }

  // Guardamos como un objeto
  tareasConHora.push({
    nombre: nombreTarea,
    hora: horaTarea
  });

  mostrarTareasConHora();

  // Limpiar los campos
  nombreCampo.value = "";
  horaCampo.value = "";
}

// Función para mostrar las tareas con su hora
function mostrarTareasConHora() {
  let lista = document.getElementById("lista_tareas_horario");
  lista.innerHTML = ""; // Limpiamos la lista primero

  tareasConHora.forEach((tarea, index) => {
    let li = document.createElement("li");
    li.classList.add("list-group-item");

    li.innerHTML = `<strong>${tarea.nombre}</strong> a las <small>${tarea.hora}</small>`;

    let botonEliminar = document.createElement("button");
    botonEliminar.textContent = "Eliminar";
    botonEliminar.classList.add("btn", "btn-danger", "btn-sm", "ms-2");

    botonEliminar.onclick = function () {
      eliminarTareaConHora(index);
    };

    li.appendChild(botonEliminar);
    lista.appendChild(li);
  });
}

// Función para eliminar una tarea con hora
function eliminarTareaConHora(index) {
  tareasConHora.splice(index, 1);
  mostrarTareasConHora();
}