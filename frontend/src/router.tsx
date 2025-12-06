import { createBrowserRouter } from "react-router-dom";
import Layout from "./components/Layout/Layout";
import OwnersPage from "./components/Owners/OwnersPage";
import PropertyPage from "./components/Property/PropertyPage";
import PropertyDetailPage from "./components/Property/PropertyDetailPage";
import TraceFormPage from "./components/Trace/TraceFormPage";

function Placeholder({ title }: { title: string }) {
  return <div style={{ padding: 16 }}>Sección: {title}</div>;
}

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      { index: true, element: <Placeholder title="Inicio" /> },
      { path: "owners", element: <OwnersPage /> },
      { path: "properties", element: <PropertyPage/> },
      { path: "properties/:id", element: <PropertyDetailPage /> },
      { path: "trace/:id", element: <TraceFormPage /> },
    ],
  },
]);