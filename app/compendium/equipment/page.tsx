'use client';

import { useState, useEffect } from "react";
import CompendiumTable from "@/components/compendiumtable";
import { Spinner } from "@heroui/spinner";
import { siteConfig } from "@/config/site";

export default function EquipmentPage() {
    const columns = ["name", "cost", "level"];
    const displayColumns = ["Name", "Cost", "Level"];
    const detailText = "description";
    const [data, setData] = useState<any[]>([]); // State to hold table data
    const [loading, setLoading] = useState<boolean>(true); // State to track loading

    useEffect(() => {
        // Fetch data from the API
        async function fetchEquipment() {
            try {
                const response = await fetch(siteConfig.links.baseApiUrl + "/GetAllEquipmentsForTable"); // Replace with your API endpoint
                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }
                const equipment = await response.json();
                setData(equipment); // Update state with fetched data
                console.log(equipment);
            } catch (error) {
                console.error("Failed to fetch equipment data:", error);
            } finally {
                setLoading(false); // Set loading to false after fetching
            }
        }

        fetchEquipment();
    }, []); // Empty dependency array ensures this runs only once

    return (
        <div>
            {loading ? (
                <Spinner />
            ) : (
                    <CompendiumTable columns={columns} displayColumns={displayColumns} data={data} detailText={detailText}></CompendiumTable>
            )}
        </div>
    );
}