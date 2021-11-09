# How to build

With Rust installed, just run `cargo build` in this directory.

If you haven't yet run `rustup target add wasm32-unknown-emscripten`, you'll be prompted to run that first.

To build binaries for a target other than WebAssembly, do:

- In `Cargo.toml`, change `crate-type = ["staticlib"]` to `crate-type = ["dylib"]`
- Run `cargo build --target x86_64-pc-windows-msvc`
- ... etc, for other platforms
