import { Link, Outlet, useLocation } from "react-router-dom";

export default function Layout() {
  const { pathname } = useLocation();

  const isActive = (path: string) =>
    pathname === path ? { background: "#111", color: "#fff" } : {};

  return (
    <div style={{ display: "grid", gridTemplateRows: "auto 1fr", height: "100vh" }}>
      <header
        style={{
          display: "flex",
          gap: 8,
          padding: 12,
          borderBottom: "1px solid #eee",
        }}
      >
        <Link to="/owners">
          <button style={{ padding: "8px 12px", ...isActive("/owners") }}>
            Owners
          </button>
        </Link>
        <Link to="/properties">
          <button style={{ padding: "8px 12px", ...isActive("/properties") }}>
            Properties
          </button>
        </Link>        
      </header>

      <main style={{ display: "grid", gridTemplateColumns: "1fr", height: "100%" }}>
        <Outlet />
      </main>
    </div>
  );
}
