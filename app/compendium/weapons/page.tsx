'use client';

import { useState, useEffect } from "react";
import CompendiumTable from "@/components/compendiumtable";

export default function WeaponsPage() {
    const columns = ["Name", "Type", "Damage", "Cost", "Concealable", "Range", "AmmoCapacity", "Radius"];
    const [data, setData] = useState<any[]>([]); // State to hold table data
    const [loading, setLoading] = useState<boolean>(true); // State to track loading

    useEffect(() => {
        // Fetch data from the API
        async function fetchWeapons() {
            try {
                const response = await fetch("https://revolutionariesrpg.com/api/GetWeaponsForTable"); // Replace with your API endpoint
                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }
                const weapons = await response.json();
                setData(weapons); // Update state with fetched data
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
                <p>Loading...</p> // Show a loading message while data is being fetched
            ) : (
                <CompendiumTable columns={columns} data={data}></CompendiumTable>
            )}
        </div>
    );
}