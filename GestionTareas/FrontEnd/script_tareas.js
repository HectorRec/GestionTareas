const boton = document.getElementById("agregar");

let tareas = [];

boton.onclick = function () {
  agregarTarea4();
};

function agregarTarea() {
  let campo = document.getElementById("tarea");

  let tarea = campo.value;

  if (tarea === "") {
    console.log("esta vácío");
    alert("Esta vacio");
  } else {
    let lista = document.getElementById("lista_tareas");
    tareas.push(tarea);
    let li = document.createElement("li");

    li.classList.add("list-group-item");

    li.textContent = tarea;

    let boton_eliminar = document.createElement("button");
    let boton_completar = document.createElement("button");

    boton_completar.innerHTML = "Completada"; //btn //btn-success
    boton_eliminar.innerHTML = "Eliminar"; //btn //btn-success

    boton_completar.classList.add("btn");
    boton_completar.classList.add("btn-primary");

    boton_eliminar.classList.add("btn");
    boton_eliminar.classList.add("btn-danger");

    li.appendChild(boton_completar);
    li.appendChild(boton_eliminar);

    boton_completar.onclick = function () {
      alert("tachad");
      li.classList.toggle("tachado");
    };

    boton_eliminar.onclick = function () {
      li.remove();
    };

    lista.appendChild(li);
  }
}

function agregarTarea2() {
  let campo = document.getElementById("tarea");

  let tarea = campo.value;

  if (tarea === "") {
    console.log("esta vácío");
  } else {
    let lista = document.getElementById("lista_tareas");
    tareas.push(tarea);
    let li = document.createElement("li");

    li.innerHTML = tarea;

    let boton_eliminar = document.createElement("button");
    let boton_completar = document.createElement("button");

    boton_completar.innerHTML = "Completada"; //btn //btn-success
    boton_eliminar.innerHTML = "Eliminar"; //btn //btn-success

    boton_completar.classList.add("btn");
    boton_completar.classList.add("btn-primary");

    boton_eliminar.classList.add("btn");
    boton_eliminar.classList.add("btn-danger");

    li.appendChild(boton_completar);
    li.appendChild(boton_eliminar);

    boton_completar.onclick = function () {
      li.classList.toggle("tachado");
    };

    lista.appendChild(li);

    boton_eliminar.onclick = function () {
      li.remove();
    };
  }
}

function agregarTareaCopia() {
  let campo = document.getElementById("tarea");

  let tarea = campo.value;

  if (tarea === "") {
    console.log("esta vácío");
  } else {
    let lista = document.getElementById("lista_tareas");
    tareas.push(tarea);
    let li = document.createElement("li");

    li.innerHTML = tarea;

    let boton_eliminar = document.createElement("button");

    boton_eliminar.innerHTML = "Eliminar"; //btn //btn-success

    boton_eliminar.classList.add("btn");
    boton_eliminar.classList.add("btn-danger");

    li.appendChild(boton_eliminar);

    li.onclick = function () {
      li.classList.toggle("tachado");
    };

    lista.appendChild(li);
  }
}

function agregarTarea4() {
  let campo = document.getElementById("tarea");
  // Eliminamos espacios en blanco porque si no da un error raro
  let tarea = campo.value.trim();

  if (tarea === "") {
    console.log("Está vacío");
    return;
  } else {
    let lista = document.getElementById("lista_tareas");

    let li = document.createElement("li");
    li.classList.add("list-group-item", "d-flex", "justify-content-between");

    // aqui separo el texto porque si no deja de funcionar el tachar tarea
    let separacionTexto = document.createElement("separacionTexto");
    separacionTexto.textContent = tarea;

    // ya que estamos creamos tambien un contenedor para los botones
    let divBotones = document.createElement("div");
    divBotones.classList.add("d-inline-flex", "gap-2");

    let boton_completar = document.createElement("button");
    let boton_eliminar = document.createElement("button");

    boton_completar.textContent = "Completada";
    boton_eliminar.textContent = "Eliminar";

    boton_completar.classList.add("btn", "btn-primary");
    boton_eliminar.classList.add("btn", "btn-danger");

    boton_completar.onclick = function () {
      separacionTexto.classList.toggle("tachado");
    };

    boton_eliminar.onclick = function () {
      li.remove();
    };

    divBotones.appendChild(boton_completar);
    divBotones.appendChild(boton_eliminar);

    li.appendChild(separacionTexto);
    li.appendChild(divBotones);
    lista.appendChild(li);
  }
}
