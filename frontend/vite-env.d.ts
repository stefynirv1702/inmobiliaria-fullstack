interface ImportMetaEnv {
  readonly VITE_API_BASE_URL: string;
  // agrega más variables aquí si lo necesitas
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}