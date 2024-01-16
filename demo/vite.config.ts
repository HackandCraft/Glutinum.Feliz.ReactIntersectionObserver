import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
//    base: "/analytics-gh-pages/",
    plugins: [react()],
    server: {
        watch: {
            ignored: [
                "**/*.fs"
            ]
        }
    },
    clearScreen: false
})
