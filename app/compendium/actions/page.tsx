'use client';

import { useState, useEffect } from "react";
import CompendiumTable from "@/components/compendiumtable";
import { Spinner } from "@heroui/spinner";
import { siteConfig } from "@/config/site";

export default function ActionsPage() {
    const columns = ["name", "actionType"];
    const displayColumns = ["Name", "Action Type"];
    const detailText = "description";
    const [data, setData] = useState<any[]>([]); // State to hold table data
    const [loading, setLoading] = useState<boolean>(true); // State to track loading

    useEffect(() => {
        // Fetch data from the API
        async function fetchActions() {
            try {
                const response = await fetch(siteConfig.links.baseApiUrl + "/GetAllActionsForTable"); // Replace with your API endpoint
                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }
                const actions = await response.json();
                setData(actions); // Update state with fetched data
                console.log(actions);
            } catch (error) {
                console.error("Failed to fetch attributes data:", error);
            } finally {
                setLoading(false); // Set loading to false after fetching
            }
        }

        fetchActions();
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