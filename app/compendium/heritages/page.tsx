'use client';

import { useState, useEffect } from "react";
import CompendiumTable from "@/components/compendiumtable";
import { Spinner } from "@heroui/spinner";
import { siteConfig } from "@/config/site";

export default function HeritagesPage() {
    const columns = ["name"];
    const displayColumns = ["Name"];
    const detailText = "description";
    const [data, setData] = useState<any[]>([]); // State to hold table data
    const [loading, setLoading] = useState<boolean>(true); // State to track loading

    useEffect(() => {
        // Fetch data from the API
        async function fetchHeritages() {
            try {
                const response = await fetch(siteConfig.links.baseApiUrl + "/GetAllHeritagesForTable"); // Replace with your API endpoint
                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }
                const heritages = await response.json();
                setData(heritages); // Update state with fetched data
                console.log(heritages);
            } catch (error) {
                console.error("Failed to fetch heritages data:", error);
            } finally {
                setLoading(false); // Set loading to false after fetching
            }
        }

        fetchHeritages();
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