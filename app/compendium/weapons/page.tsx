'use client';

import { useState, useEffect } from "react";
import CompendiumTable from "@/components/compendiumtable";
import { Spinner } from "@heroui/spinner";
import { siteConfig } from "@/config/site";

export default function WeaponsPage() {
    const columns = ["name", "type", "damage", "cost", "concealable", "range", "ammoCapacity", "radius"];
    const displayColumns = ["Name", "Type", "Damage", "Cost", "Concealable", "Range", "Ammo Capacity", "Radius"];
    const detailText = "description";
    const [data, setData] = useState<any[]>([]); // State to hold table data
    const [loading, setLoading] = useState<boolean>(true); // State to track loading

    useEffect(() => {
        // Fetch data from the API
        async function fetchWeapons() {
            try {
                const response = await fetch(siteConfig.links.baseApiUrl + "/GetWeaponsForTable"); // Replace with your API endpoint
                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }
                const weapons = await response.json();
                setData(weapons); // Update state with fetched data
                console.log(weapons);
            } catch (error) {
                console.error("Failed to fetch weapons data:", error);
            } finally {
                setLoading(false); // Set loading to false after fetching
            }
        }

        fetchWeapons();
    }, []); // Empty dependency array ensures this runs only once

    return (
        <div>
            {loading ? (
                <Spinner/>
            ) : (
                <CompendiumTable columns={columns} displayColumns={displayColumns} data={data} detailText={detailText} ></CompendiumTable>
            )}
        </div>
    );
}