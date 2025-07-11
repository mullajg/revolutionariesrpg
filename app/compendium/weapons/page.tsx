'use client';

import { title } from "@/components/primitives";
import CompendiumTable from "@/components/compendiumtable"; 

export default function WeaponsPage() {
    const columns = ["Name", "Type", "Damage", "Cost", "Concealable", "Range", "AmmoCapacity", "Radius"];
    const data = [
        {
            Name: "M45 High Power",
            Type: "Ranged",
            Damage: "d4+4",
            Cost: "50",
            Concealable: "Yes",
            Range: "13",
            AmmoCapacity: "10",
            Radius: "N/A"
        }
    ];

    return (
        <div>
            <CompendiumTable columns={columns} data={data}></CompendiumTable>
        </div>
    );
}
