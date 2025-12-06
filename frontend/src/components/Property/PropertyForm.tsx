import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQuery } from "@tanstack/react-query";
import { createProperty } from "../../service/propertyService";
import { getOwnersList } from "../../service/ownersService";
import type { Owner, OwnerList } from "../../types/owner";
import "../../assets/css/Form.css";

const schema = z.object({
    name: z.string().min(2, "Nombre muy corto"),    
    image: z.string().min(2, "Nombre muy corto"),
    idOwner: z.string().min(1, "Debe seleccionar un propietario"),
    address: z.string().min(5, "La dirección es muy corta"),
    year: z.number().int("El año debe ser un número entero").min(1900, "El año no puede ser anterior a 1900").max(new Date().getFullYear(), "El año no puede ser en el futuro"),    
    price: z.number()
});

type FormValues = z.infer<typeof schema>;

export default function PropertyForm({ onSuccess }: { onSuccess: () => void }) {    

    const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<FormValues>({
        resolver: zodResolver(schema),
    });
  
    const { data: owners, isLoading: isLoadingOwners, isError: isErrorOwners } = useQuery<Owner[]>({
        queryKey: ["owners"],
        queryFn: getOwnersList,
    });

    const mutation = useMutation({
        mutationFn: createProperty,
        onSuccess: () => onSuccess(),
    });   
   
    const onSubmit = (values: FormValues) => {
        debugger;
        mutation.mutate(values);
    };

    return (
        <>
            <form className="form-container" onSubmit={handleSubmit(onSubmit)} style={{ display: "grid", gap: 12 }}>
                <h3>Nuevo Property</h3>

                <label>
                    Nombre
                    <input type="text" {...register("name")} />
                    {errors.name && <span className="error-message" style={err}>{errors.name.message}</span>}
                </label>

                <label>
                    Dirección
                    <input type="text" {...register("address")} />
                    {errors.address && <span className="error-message" style={err}>{errors.address.message}</span>}
                </label>
               
                <label>
                    Price
                    <input type="number" {...register("price", { valueAsNumber: true })} />
                    {errors.price && <span className="error-message" style={err}>{errors.price.message}</span>}
                </label>

                 <label>
                    Propietario
                    {isLoadingOwners && <span>Cargando propietarios...</span>}
                    {isErrorOwners && <span style={err}>Error al cargar propietarios</span>}
                    {!isLoadingOwners && !isErrorOwners && (
                    
                    <select {...register("idOwner")} defaultValue="" style={err}>
                        <option value="" disabled>Seleccione un propietario</option>
                        {(owners as OwnerList[] | Owner[]).map((owner : OwnerList | Owner) => ( 
                        <option key={owner.idOwner} value={owner.idOwner}>
                            {owner.name}
                        </option>
                        ))}
                    </select>
                    )}
                    {errors.idOwner && <span style={err}>{errors.idOwner.message}</span>}
                </label>

                <label>
                    Foto (URL Opcional)
                    <input type="text" {...register("image")} style={err} />
                    {errors.image && <span style={err}>{errors.image.message}</span>}
                </label>

                <label>
                    Año
                    <input type="number" {...register("year", { valueAsNumber: true })} style={err} />
                    {errors.year && <span style={err}>{errors.year.message}</span>}
                </label>

                <button type="submit" disabled={isSubmitting || mutation.isPending}>
                    {mutation.isPending ? "Guardando..." : "Guardar"}
                </button>
                    
                {mutation.isError && (
                    <p className="mutation-error" style={{ color: "red" }}>
                        {(mutation.error as Error)?.message ?? "Error al guardar"}
                    </p>
                )}
            </form> 
        </>
    );
}

const err: React.CSSProperties = { color: "crimson", display: "block" };
