use std::ffi::CStr;
use std::os::raw::{c_uchar};
use core::slice;

#[no_mangle]
extern "C" fn generate_qr_code(c_str: *const i8, bytes: *mut c_uchar, byte_length: usize, out_width: *mut usize, out_height: *mut usize) -> () {
    let value = unsafe { CStr::from_ptr(c_str).to_str().unwrap() };

    // Generate the QR code
    let qr_code = qr_code::QrCode::new(value).unwrap();

    // Convert it to RGBA format (because that's what HTML <canvas> requires)
    let buffer = unsafe {
        *out_width = qr_code.width();
        *out_height = qr_code.width();
        slice::from_raw_parts_mut(bytes, byte_length)
    };

    let mut pos = 0;
    for pixel_set in qr_code.into_colors() {
        let pixel_value = if pixel_set == qr_code::Color::Light { 255 } else { 0 };
        buffer[pos] = pixel_value;   // Red
        buffer[pos+1] = pixel_value; // Green
        buffer[pos+2] = pixel_value; // Blue
        buffer[pos+3] = 255;         // Alpha
        pos += 4;
    }
}
