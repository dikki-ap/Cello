import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import version from 'vite-plugin-package-version'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react(), version()],
    server: {
        port: 5173,
        https: false,
        strictPort: true,
        proxy: {
            "/api": {
                target: "http://localhost:5067",
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/api/, "/api"),
            }
        }

    }
});
