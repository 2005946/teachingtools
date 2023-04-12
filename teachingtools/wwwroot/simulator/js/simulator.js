import { activeTool, currMouseAction } from "./menutools.js"
import { MouseAction } from "./circuit_components/Enums.js"
import { WireManager } from "./circuit_components/Wire.js";
import { FileManager } from "./FileManager.js"

export let gateIMG = []; // gates images
export let gate = [];
export let logicInput = [];
export let logicOutput = [];
export let wireMng;
export let colorMouseOver = [0 ,0x7B, 0xFF];
export let fileManager = new FileManager();

/**
 * todo TODO
 */
export function preload() {
    gateIMG.push(loadImage('simulator/img/LogicInput.svg'));// For testing usage
    gateIMG.push(loadImage('simulator/img/NOT.svg'));
    gateIMG.push(loadImage('simulator/img/AND.svg'));
    gateIMG.push(loadImage('simulator/img/NAND.svg'));
    gateIMG.push(loadImage('simulator/img/OR.svg'));
    gateIMG.push(loadImage('simulator/img/NOR.svg'));
    gateIMG.push(loadImage('simulator/img/XOR.svg'));
    gateIMG.push(loadImage('simulator/img/XNOR.svg'));
}

/**
 * todo TODO
 */
export function setup() {
    const canvHeight = windowHeight - 90;
    let canvas = createCanvas(windowWidth, canvHeight, P2D);

    canvas.parent('canvas-sim');
    document.getElementsByClassName("tools")[0].style.height = canvHeight;

    wireMng = new WireManager();
}

/**
 * todo TODO
 */
export function windowResized() {
    const canvHeight = windowHeight - 90;
    resizeCanvas(windowWidth, canvHeight);
    document.getElementsByClassName("tools")[0].style.height = canvHeight;
}

/**
 * todo TODO
 */
export function draw() {
    background(0xFF);
    stroke(0);
    strokeWeight(4);
    fill(0xFF)
    rect(0, 0, width, height);
    
    wireMng.draw();

    for (let i = 0; i < gate.length; i++)
        gate[i].draw();

    for (let i = 0; i < logicInput.length; i++)
        logicInput[i].draw();

    for (let i = 0; i < logicOutput.length; i++)
        logicOutput[i].draw();

    if(fileManager.isLoadingState)
        fileManager.isLoadingState = false;

}

/**
 * While mouse is pressed:
 *  
 */
export function mousePressed() {
    /** Check gate[] mousePressed funtion*/
    for (let i = 0; i < gate.length; i++)
        gate[i].mousePressed();

    for (let i = 0; i < logicInput.length; i++)
        logicInput[i].mousePressed();

    for (let i = 0; i < logicOutput.length; i++)
        logicOutput[i].mousePressed();
}

/**
 * @todo TODO
 */
export function mouseReleased() {
    for (let i = 0; i < gate.length; i++)
        gate[i].mouseReleased();

    for (let i = 0; i < logicInput.length; i++)
        logicInput[i].mouseReleased();

    for (let i = 0; i < logicOutput.length; i++)
        logicOutput[i].mouseReleased();
}

/**
 * todo TODO
 */
export function doubleClicked() {
    for (let i = 0; i < logicInput.length; i++)
        logicInput[i].doubleClicked();
}

/**
 * Override mouseClicked Function
 * 
 */
export function mouseClicked() {
    //Check current selected option
    if (currMouseAction == MouseAction.EDIT) {
        //If action is EDIT, check every class. 
        for (let i = 0; i < gate.length; i++)
            gate[i].mouseClicked();

        for (let i = 0; i < logicInput.length; i++)
            logicInput[i].mouseClicked();

        for (let i = 0; i < logicOutput.length; i++)
            logicOutput[i].mouseClicked();

    } else if (currMouseAction == MouseAction.DELETE) {
        //
        for (let i = 0; i < gate.length; i++) {
            if (gate[i].mouseClicked()) {
                gate[i].destroy();
                delete gate[i];
                gate.splice(i, 1);
            }
        }

        for (let i = 0; i < logicInput.length; i++) {
            if (logicInput[i].mouseClicked()) {
                logicInput[i].destroy();
                delete logicInput[i];
                logicInput.splice(i, 1);
            }
        }

        for (let i = 0; i < logicOutput.length; i++) {
            if (logicOutput[i].mouseClicked()) {
                logicOutput[i].destroy();
                delete logicOutput[i];
                logicOutput.splice(i, 1);
            }
        }
    }
    wireMng.mouseClicked();
}

window.preload = preload;
window.setup = setup;
window.draw = draw;
window.windowResized = windowResized;
window.mousePressed = mousePressed;
window.mouseReleased = mouseReleased;
window.doubleClicked = doubleClicked;
window.mouseClicked = mouseClicked;

window.activeTool = activeTool;

document.getElementById("projectFile").addEventListener("change", fileManager.loadFile, false);
document.getElementById("saveProjectFile").addEventListener("click", fileManager.saveFile, false);

/**
 * Call FileManager.saveFile
 */
export function saveFile()
{
    fileManager.saveFile();
}