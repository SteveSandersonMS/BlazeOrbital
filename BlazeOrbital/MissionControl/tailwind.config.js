module.exports = {
  purge: [
    './src/**/*.{js,jsx,ts,tsx}',
    './public/index.html',
    '../ManufacturingHub/**/*.razor',
    '../ManufacturingHub/**/*.html'
  ],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {},
  },
  variants: {
    extend: {
      opacity: ['active'],
    },
  },
  plugins: [],
}
