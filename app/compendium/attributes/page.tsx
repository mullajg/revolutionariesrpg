'use client';

import { useState, useEffect } from "react";
import CompendiumTable from "@/components/compendiumtable";
import { Spinner } from "@heroui/spinner";
import { siteConfig } from "@/config/site";

export default function AttributesPage() {
    const columns = ["name", "shortName"];
    const displayColumns = ["Name", "Short Name"];
    const detailText = "description";
    const [data, setData] = useState<any[]>([]); // State to hold table data
    const [loading, setLoading] = useState<boolean>(true); // State to track loading

    useEffect(() => {
        // Fetch data from the API
        async function fetchAttributes() {
            try {
                const response = await fetch(siteConfig.links.baseApiUrl + "/GetAllAttributesForTable"); // Replace with your API endpoint
                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }
                const attributes = await response.json();
                setData(attributes); // Update state with fetched data
                console.log(attributes);
            } catch (error) {
                console.error("Failed to fetch attributes data:", error);
            } finally {
                setLoading(false); // Set loading to false after fetching
            }
        }

        fetchAttributes();
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