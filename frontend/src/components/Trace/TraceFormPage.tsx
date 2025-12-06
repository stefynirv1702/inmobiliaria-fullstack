import { useParams, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { createTrace } from "../../service/traceService";
import type { CreateTraceRequest} from "../../types/trace";
import "../../assets/css/Form.css";



export default function TraceFormPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { register, handleSubmit, formState: { errors } } = useForm<CreateTraceRequest>({
    defaultValues: {
      propertyId: id ?? "",
      name: "",
      value: 0,
      tax: 0,
    },
  });

  const onSubmit = async (data: CreateTraceRequest) => {
    try {        
      data.propertyId = id ?? "";
      await createTrace(data);
      alert("Successfully created ✅");
      navigate(`/properties/${id}`);
    } catch (err) {
      alert("Error creating the trace ❌");
      console.error(err);
    }
  };

  return (
    <div style={{ padding: 16 }}>
      <h2>Add Trace</h2>
      <form className="form-container" onSubmit={handleSubmit(onSubmit)} style={{ display: "grid", gap: 12 }}>
        <input type="hidden" {...register("propertyId")} />

        <label>
          Name
          <input type="text" {...register("name", { required: true })} />
          {errors.name && <p style={{ color: "red" }}>Name is required</p>}
        </label>

        <label>
          Value
          <input type="number" {...register("value", { required: true, min: 1 })} />
          {errors.value && <p style={{ color: "red" }}>Value must be greater than 0</p>}
        </label>

        <label>
          Tax
          <input type="number" {...register("tax", { required: true, min: 0 })} />
          {errors.tax && <p style={{ color: "red" }}>Tax is required</p>}
        </label>

        <button type="submit" style={btn}>Save</button>
        <button type="button" onClick={() => navigate(-1)} style={{ ...btn, background: "gray" }}>
          Cancel
        </button>
      </form>
    </div>
  );
}

const btn: React.CSSProperties = {
  padding: "8px 16px",
  background: "#28a745",
  color: "white",
  border: "none",
  borderRadius: "6px",
  cursor: "pointer",
};
