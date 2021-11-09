The files in this directory can be used to regenerate `../ManufacturingHub/wwwroot/css/tailwind.bundled.min.css`.

To do so, run `npm install` then `npm run build`.

Although it's slightly unusual to generate a copy of Tailwind purely based on the `purge` patterns in `tailwind.config.js` (rather than generating a combination of your own app CSS files and Tailwind), this approach means it's unnecessary to run this tooling on each build. Instead, you can run it on demand whenever you think you may have started using some new Tailwind CSS rule.
