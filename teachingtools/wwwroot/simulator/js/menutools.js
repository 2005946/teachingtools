import { logicInput, logicOutput, gate } from "./simulator.js";
import { Gate } from "./circuit_components/Gate.js";
import { LogicInput } from "./circuit_components/LogicInput.js";
import { LogicOutput } from "./circuit_components/LogicOutput.js";
import { MouseAction } from "./circuit_components/Enums.js"

export let currMouseAction = MouseAction.EDIT;

/**
 * @todo TODO
 */
export function activeTool(elTool) {
    resetElements();
    if (elTool.getAttribute("isGate") != null) {
        gate.push(new Gate(elTool.getAttribute("tool")));
        return;
    }

    switch (elTool.getAttribute("tool")) {
        case "Edit":
            resetElements();
            break;

        case "Move":
            currMouseAction = MouseAction.MOVE;
            document.getElementById("canvas-sim").style.cursor = "move";
            break;

        case "Delete":
            currMouseAction = MouseAction.DELETE;
            break;

        case "LogicInput":
            logicInput.push(new LogicInput());
            console.log(JSON.stringify({ logicInput }, ['logicInput', 'posX', 'posY', 'value']));
            break;

        case "LogicOutput":
            logicOutput.push(new LogicOutput());
            break;
    }

    elTool.classList.add('active');

}

/**
 * @todo this doc
 */
function resetElements() {
    currMouseAction = MouseAction.EDIT;
    let activeElements = document.getElementsByClassName("active");

    for (let i = 0; i < activeElements.length; i++) {
        activeElements[i].classList.remove('active')
    }
    document.getElementById("canvas-sim").style.cursor = "default";
}

/**
 * Reset Element
 * then set current action to EDIT 
 */
export function backToEdit() {
    resetElements();
    document.getElementsByClassName("Edit")[0].classList.add("active");
    currMouseAction = MouseAction.EDIT;
}

/**
 * Save Option
 * Export All logic input and output on console
 */
export function save() {

    for (let i = 0; i < logicInput.length; i++) {
        console.log(logicInput[i].export());
    }
}