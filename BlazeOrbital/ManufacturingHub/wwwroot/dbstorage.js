export function synchronizeFileWithIndexedDb(filename) {
    return new Promise((res, rej) => {
        const db = window.indexedDB.open('SqliteStorage', 1);
        db.onupgradeneeded = () => {
            db.result.createObjectStore('Files', { keypath: 'id' });
        };

        db.onsuccess = () => {
            const req = db.result.transaction('Files', 'readonly').objectStore('Files').get('file');
            req.onsuccess = () => {
                Module.FS_createDataFile('/', filename, req.result, true, true, true);
                res();
            };
        };

        let lastModifiedTime = new Date();
        setInterval(() => {
            const path = `/${filename}`;
            if (FS.analyzePath(path).exists) {
                const mtime = FS.stat(path).mtime;
                if (mtime.valueOf() !== lastModifiedTime.valueOf()) {
                    lastModifiedTime = mtime;
                    const data = FS.readFile(path);
                    db.result.transaction('Files', 'readwrite').objectStore('Files').put(data, 'file');
                }
            }
        }, 1000);
    });
}

window.bufferToCanvas = function(elem, buffer, width, height) {
    let imageData = new ImageData(new Uint8ClampedArray(buffer.buffer, 0, width * height * 4), width, height);
    elem.width = width;
    elem.height = height;
    elem.getContext('2d').putImageData(imageData, 0, 0);
}
