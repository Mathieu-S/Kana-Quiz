module.exports = {
  purge: {
    mode: "all",
    content: ["./src/**/*.html", "./src/**/*.liquid", "./src/js/**/*.js"],
  },
  future: {
    removeDeprecatedGapUtilities: true,
  },
  plugins: [require("@tailwindcss/custom-forms")],
};
