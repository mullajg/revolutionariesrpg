'use client';

import { useState, useEffect } from "react";
import CompendiumTable from "@/components/compendiumtable";
import { Spinner } from "@heroui/spinner";
import { siteConfig } from "@/config/site";

export default function SpellsPage() {
    const columns = ["name"];
    const displayColumns = ["Name"];
    const detailText = "description";
    const [data, setData] = useState<any[]>([]); // State to hold table data
    const [loading, setLoading] = useState<boolean>(true); // State to track loading

    useEffect(() => {
        // Fetch data from the API
        async function fetchSpells() {
            try {
                const response = await fetch(siteConfig.links.baseApiUrl + "/GetAllSpellsForTable"); // Replace with your API endpoint
                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }
                const spells = await response.json();
                setData(spells); // Update state with fetched data
                console.log(spells);
            } catch (error) {
                console.error("Failed to fetch spells data:", error);
            } finally {
                setLoading(false); // Set loading to false after fetching
            }
        }

        fetchSpells();
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