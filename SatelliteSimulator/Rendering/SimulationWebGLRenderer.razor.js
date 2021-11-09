import { default as Stats } from '/stats.module.js';

// This takes quite a few shortcuts by hardcoding stuff and not worrying too hard about
// generality and optimization. That's because GPU use isn't the point of this demo.

const vs = `
  attribute vec4 position;
  attribute vec2 satellitePosition;
  attribute vec4 color;
  
  varying vec4 v_color;
  
  void main() {
    gl_PointSize = 5.0;
    gl_Position = (position + vec4(satellitePosition, 0, 0)) * vec4(1.0/10000.0,1.0/10000.0,1,1);
    v_color = color;
  }`;

const fs = `
  precision mediump float;
  varying vec4 v_color;

  void main() {
    gl_FragColor = v_color;
  }
  `;

export function init(elem, component) {
    const gl = elem.getContext('webgl');
    const ext = gl.getExtension('ANGLE_instanced_arrays');
    if (!ext) {
        throw new error('need ANGLE_instanced_arrays');
    }
    twgl.addExtensionsToContext(gl);

    const programInfo = twgl.createProgramInfo(gl, [vs, fs]);
    const sunRadius = 300;
    const circleVertices = new Float32Array([0, 0].concat(Array.from(Array(33).keys()).flatMap(x => [sunRadius * Math.sin(x * 2 * Math.PI / 32), sunRadius * Math.cos(x * 2 * Math.PI / 32)])));
    const pointVertices = new Float32Array([0, 0]);
    const bufferInfo = twgl.createBufferInfoFromArrays(gl, {
        position: {
            numComponents: 2,
            data: []
        },
        satellitePosition: {
            numComponents: 2,
            data: [],
            divisor: 1,
            stride: 36,
        },
        color: {
            numComponents: 4,
            data: [],
            divisor: 1,
            stride: 36,
            offset: 20,
        }
    });

    twgl.setBuffersAndAttributes(gl, programInfo, bufferInfo);

    twgl.resizeCanvasToDisplaySize(gl.canvas);
    if (gl.canvas.width > gl.canvas.height) {
        const offset = (gl.canvas.width - gl.canvas.height) / 4;
        gl.viewport(-gl.canvas.width, -gl.canvas.width - offset, 2 * gl.canvas.width, 2 * gl.canvas.width - offset);
    } else {
        const offset = (gl.canvas.height - gl.canvas.width) / 4;
        gl.viewport(-gl.canvas.height - offset, -gl.canvas.height, 2 * gl.canvas.height - offset, 2 * gl.canvas.height);
    }
    
    gl.clearColor(0, 0, 0, 1);

    const stats = new Stats();
    stats.showPanel(0); // 0: fps, 1: ms, 2: mb, 3+: custom
    stats.dom.style.left = '';
    stats.dom.style.right = '0px';
    stats.dom.style.transform = 'scale(1.5)';
    stats.dom.style.transformOrigin = 'top right';
    stats.dom.style.opacity = '1';
    document.body.appendChild(stats.dom);

    let disposed = false;
    const context = {
        disposeContext() {
            stats.dom.remove();
            disposed = true;
        },
        onNextFrame(callbackName) {
            window.requestAnimationFrame(() => {
                if (!disposed) {
                    stats.begin();
                    component.invokeMethod(callbackName);
                    stats.end();
                }
            });
        },
        renderSuns(suns) {
            gl.clear(gl.COLOR_BUFFER_BIT);

            const count = Blazor.platform.getArrayLength(suns);
            const stride = bufferInfo.attribs.satellitePosition.stride;
            const data = new Float32Array(Module.HEAP8.buffer, Blazor.platform.getArrayEntryPtr(suns, 0, stride), count * stride);

            twgl.setAttribInfoBufferFromArray(gl, bufferInfo.attribs.position, circleVertices);
            twgl.setAttribInfoBufferFromArray(gl, bufferInfo.attribs.satellitePosition, data);
            twgl.setAttribInfoBufferFromArray(gl, bufferInfo.attribs.color, data);

            gl.useProgram(programInfo.program);
            ext.drawArraysInstancedANGLE(gl.TRIANGLE_FAN, 0, circleVertices.length, count);
        },
        renderSatellites(satellites) {
            const count = Blazor.platform.getArrayLength(satellites);
            const stride = bufferInfo.attribs.satellitePosition.stride;
            const data = new Float32Array(Module.HEAP8.buffer, Blazor.platform.getArrayEntryPtr(satellites, 0, stride), count * stride);

            twgl.setAttribInfoBufferFromArray(gl, bufferInfo.attribs.position, pointVertices);
            twgl.setAttribInfoBufferFromArray(gl, bufferInfo.attribs.satellitePosition, data);
            twgl.setAttribInfoBufferFromArray(gl, bufferInfo.attribs.color, data);

            gl.useProgram(programInfo.program);
            ext.drawArraysInstancedANGLE(gl.POINTS, 0, 1, count);
        },
    };
    return context;
}
